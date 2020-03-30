 
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TrualityEngine.Core
{
    public class MultiTileset<TKey> : SingleTileset
    {

        public TilesetSortMode TilsetSortMode { get; set; }
        public TKey[] Keys { get; set; }

        public Dictionary<TKey, Sprite[]> GetKeysPack()
        {
            if (Keys == null)
                throw new ArgumentNullException(nameof(Keys));
            if (Texture == null)
                throw new ArgumentNullException(nameof(Texture));
            int n = 0;
            Dictionary<TKey, Sprite[]> dictionary = new Dictionary<TKey, Sprite[]>();

            foreach (Sprite[] ar in
                TilesetLoader.ToGroupTextures(GetAsset().Value, OneCellSize, TilsetSortMode))
            {
                dictionary.Add(Keys[n], new Sprite[ar.Length]);
                int m = 0;
                foreach (Sprite txt in ar)
                {

                    dictionary[Keys[n]][m] = txt;
                    m++;
                }
                n++;
            }
            return dictionary;


        }
    }
}
