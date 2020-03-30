 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
	public class TilesetLoader
	{
		public static IEnumerable<Sprite> ToSinglesTextures(Texture2D texture, Point oneSize)
		{
			Color[,] baseT = texture.To2DimensionArray();
			int quanity = (int)((texture.Width * texture.Height) / ((double)oneSize.X * oneSize.Y));
			int quanityX = texture.Width / oneSize.X;
			int quanityY = texture.Height / oneSize.Y;

			for (int n = 1; n <= quanity; n++)
			{
				Color[,] txt = new Color[texture.Width, texture.Height];
				for (int x = ((n - 1) % quanityX) * oneSize.X, ox = 0; ox < oneSize.X; x++, ox++)
					for (int oy = 0, y = ((n - 1) - ((n - 1) % quanityY)) / quanityY * oneSize.Y; oy < oneSize.Y; y++, oy++)
						txt[ox, oy] = baseT[x, y];

				yield return Sprite.CreateAnonymous(SpriteCreator.CreateFrom2DimensionArray(txt));
			}
		}

		public static IEnumerable<Sprite[]> ToGroupTexturesVectical(Texture2D texture, Point oneSize)
		{

			int quanityX = texture.Width / oneSize.X;
			IEnumerable<Sprite> textures = ToSinglesTextures(texture, oneSize);
			Sprite[] actual = new Sprite[quanityX];
			int lastQuanity = 0;
			int i = 0;
			foreach (Sprite txt in textures)
			{

				actual[i] = txt;
				i++;
				if (i >= quanityX)
				{
					lastQuanity = i / quanityX;
					yield return actual;
					i = 0;
					actual = new Sprite[quanityX];

				}
			}
			yield break;

		}
		public int GetQuanity(Texture2D txt, Point oneSize)
		{
			return (int)((txt.Width * txt.Height) / ((double)oneSize.X * oneSize.Y));
		}
		public static IEnumerable<Sprite[]> ToGroupTexturesHorizontal(Texture2D texture, Point oneSize)
		{
			int quanitY = texture.Height / oneSize.Y;
			int quanityX = texture.Width / oneSize.X;
			Sprite[] textures = ToSinglesTextures(texture, oneSize).ToArray();
			for (int n = 0; n < quanityX; n++)
			{
				Sprite[] actual = new Sprite[quanitY];
				for (int m = 0; m < quanitY; m++)
				{
					actual[m] = textures[n + (m * quanityX)];
				}
				yield return actual;

			}


		}
		public static IEnumerable<Sprite[]> ToGroupTextures(Texture2D texture, Point oneSize, TilesetSortMode sortMode)
			=> sortMode switch
			{
				TilesetSortMode.Horizontal => ToGroupTexturesHorizontal(texture, oneSize),
				TilesetSortMode.Vertical => ToGroupTexturesVectical(texture, oneSize),
				_ => throw new ArgumentException("Unknown value", nameof(sortMode))
			};
	}
}
