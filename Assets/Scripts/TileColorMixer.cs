using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.paintpuzzle
{
    public static class TileColorUtility
    {
        //A Table that defines the color mixing result based on two tileColor input
        private static Dictionary<MixInput, TileColor> s_mixTable;

        static TileColorUtility()
        {
            //Initialize the mixing table
            s_mixTable = new Dictionary<MixInput, TileColor>();

            #region Fill the table

            s_mixTable.Add(new MixInput(TileColor.White, TileColor.Red), TileColor.Red);
            s_mixTable.Add(new MixInput(TileColor.White, TileColor.Green), TileColor.Green);
            s_mixTable.Add(new MixInput(TileColor.White, TileColor.Blue), TileColor.Blue);

            s_mixTable.Add(new MixInput(TileColor.Red, TileColor.Red), TileColor.Red);
            s_mixTable.Add(new MixInput(TileColor.Red, TileColor.Green), TileColor.Yellow);
            s_mixTable.Add(new MixInput(TileColor.Red, TileColor.Blue), TileColor.Magenta);

            s_mixTable.Add(new MixInput(TileColor.Green, TileColor.Red), TileColor.Yellow);
            s_mixTable.Add(new MixInput(TileColor.Green, TileColor.Green), TileColor.Green);
            s_mixTable.Add(new MixInput(TileColor.Green, TileColor.Blue), TileColor.Cyan);

            s_mixTable.Add(new MixInput(TileColor.Blue, TileColor.Red), TileColor.Magenta);
            s_mixTable.Add(new MixInput(TileColor.Blue, TileColor.Green), TileColor.Cyan);
            s_mixTable.Add(new MixInput(TileColor.Blue, TileColor.Blue), TileColor.Blue);

            s_mixTable.Add(new MixInput(TileColor.Yellow, TileColor.Red), TileColor.Yellow);
            s_mixTable.Add(new MixInput(TileColor.Yellow, TileColor.Green), TileColor.Yellow);
            s_mixTable.Add(new MixInput(TileColor.Yellow, TileColor.Blue), TileColor.White);

            s_mixTable.Add(new MixInput(TileColor.Magenta, TileColor.Red), TileColor.Magenta);
            s_mixTable.Add(new MixInput(TileColor.Magenta, TileColor.Green), TileColor.White);
            s_mixTable.Add(new MixInput(TileColor.Magenta, TileColor.Blue), TileColor.Magenta);

            s_mixTable.Add(new MixInput(TileColor.Cyan, TileColor.Red), TileColor.White);
            s_mixTable.Add(new MixInput(TileColor.Cyan, TileColor.Green), TileColor.Cyan);
            s_mixTable.Add(new MixInput(TileColor.Cyan, TileColor.Blue), TileColor.Cyan);

            #endregion
        }

        public static TileColor MixColor(TileColor background, TileColor forground) => s_mixTable[new MixInput(background, forground)];

        public static Color32 GetColor(TileColor tileColor)
        {
            switch (tileColor)
            {
                case TileColor.White:
                    return Color.white;
                case TileColor.Red:
                    return Color.red;
                case TileColor.Green:
                    return Color.green;
                case TileColor.Blue:
                    return Color.blue;
                case TileColor.Yellow:
                    return Color.yellow;
                case TileColor.Magenta:
                    return Color.magenta;
                case TileColor.Cyan:
                    return Color.cyan;
                default:
                    throw new ArgumentException();
            }
        }

        private struct MixInput
        {
            private readonly TileColor _background;
            private readonly TileColor _forground;

            public MixInput(TileColor background, TileColor forground)
            {
                _background = background;
                _forground = forground;
            }
        }
    }
}