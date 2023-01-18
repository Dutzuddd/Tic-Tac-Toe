using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        //Holds the curent result of cells in the current game
        private MarkType[] mResult;
        //True-if its player(X) turn, False if it`s player(0) turn
        private bool mPlayerTurn;
        //True if the game has ended
        private bool mGameEnded;
        #endregion

        #region Construnctor

        //Defaul constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
            

        }
        #endregion

        //start a new game
        private void NewGame()
        {
            mResult = new MarkType[9];

            for(var i = 0; i < mResult.Length; i++)
                mResult[i] = MarkType.Free;

            //Player (X) starts the game
            mPlayerTurn = true;
            //iterate every button on the grit
            Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    //default values for backkgr.,foregr. and content
                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                });


            //make sure the game hasn`t ended
            mGameEnded = false;

        }

        //handles a button click event
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;

            //find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //check if cell has a value
            if (mResult[index] != MarkType.Free)
                return;
            //set cell value based on which players turn is it
            mResult[index] = mPlayerTurn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayerTurn ? "X" : "0";

            if (!mPlayerTurn)
                button.Foreground = Brushes.Red;
            //toggle players turns
            mPlayerTurn ^= true;

            CheckForWinner();
        }

        //function that checks for winner(3-line straight)
        private void CheckForWinner()
        {
            //check for horizontal wins
            // row 0
            if(mResult[0] != MarkType.Free && (mResult[0] & mResult[1] & mResult[2]) == mResult[0])
            {
                //game ends
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }
            //check for horizontal wins
            // row 1
            if (mResult[3] != MarkType.Free && (mResult[3] & mResult[4] & mResult[5]) == mResult[3])
            {
                //game ends
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }
            //check for horizontal wins
            // row 2
            if (mResult[6] != MarkType.Free && (mResult[6] & mResult[7] & mResult[8]) == mResult[6])
            {
                //game ends
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }

            //check for vertical wins
            // column 0
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[3] & mResult[6]) == mResult[0])
            {
                //game ends
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }
            // column 1
            if (mResult[1] != MarkType.Free && (mResult[1] & mResult[4] & mResult[7]) == mResult[1])
            {
                //game ends
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }
            // column 2
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[5] & mResult[8]) == mResult[2])
            {
                //game ends
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }

            //check for vertical wins
            // diagonal 1
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[4] & mResult[8]) == mResult[0])
            {
                //game ends
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }

            // diagonal 2
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[4] & mResult[6]) == mResult[2])
            {
                //game ends
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }

            //check for no winner and full board
            if (!mResult.Any(f => f == MarkType.Free))
            {
                //game ended
                mGameEnded = true;

                //turns cells to another color
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {                   
                    button.Background = Brushes.LightYellow;                    
                });

            }
        
        }
    }
}
