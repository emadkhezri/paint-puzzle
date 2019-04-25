using UnityEngine;

namespace Assets.Code.Classes.Command
{
    class AddColorCommand : ICommand
    {
        private AddColorCommandParams commandParams;
        private Color[,] colorsBeforeExecution;

        public AddColorCommand(AddColorCommandParams commandParams)
        {
            this.commandParams = commandParams;
            colorsBeforeExecution = new Color[commandParams.PuzzleBoard.GetLength(0), commandParams.PuzzleBoard.GetLength(1)];

            for (int x = (int)commandParams.SelectionRect.xMin; x <= commandParams.SelectionRect.xMax; x++)
                for (int y = (int)commandParams.SelectionRect.yMin; y <= commandParams.SelectionRect.yMax; y++)
                    colorsBeforeExecution[x, y] = commandParams.PuzzleBoard[x, y].GetComponent<PuzzleCellScript>().ActualColor;
        }

        public void Execute()
        {
            for (int x = (int)commandParams.SelectionRect.xMin; x <= commandParams.SelectionRect.xMax; x++)
                for (int y = (int)commandParams.SelectionRect.yMin; y <= commandParams.SelectionRect.yMax; y++)
                    commandParams.PuzzleBoard[x, y].GetComponent<PuzzleCellScript>().AddColor(commandParams.SelectedColor);
        }

        public void Unexecute()
        {
            for (int x = (int)commandParams.SelectionRect.xMin; x <= commandParams.SelectionRect.xMax; x++)
                for (int y = (int)commandParams.SelectionRect.yMin; y <= commandParams.SelectionRect.yMax; y++)
                    commandParams.PuzzleBoard[x, y].GetComponent<PuzzleCellScript>().ActualColor = colorsBeforeExecution[x, y];
        }
    }

    class AddColorCommandParams
    {
        public GameObject[,] PuzzleBoard { get; set; }
        public Rect SelectionRect { get; set; }
        public Color SelectedColor { get; set; }
    }

}
