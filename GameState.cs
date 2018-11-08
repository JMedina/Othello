using UnityEngine
using System.Collections;

public class GameState : Monobehaviour
{
   char [,] board = new char [8,8];  //col, then row
   char whosTurn;


   void Start () 
   {
      for (int col = 0; col < 8, ++col)
      {
         for (int row = 0, row < 8; ++row)
         {
            board [col][row] = ' ';
         }
      }

      board [3][3] = 'w';
      board [4][4] = 'w';

      board [3][4] = 'b';
      board [4][3] = 'b';

      whosTurn = 'b';
   }


   void Update 
   {
      //Check if the current player has moves available
      // We don't mind taking a long time for this because the player will be
      // busy making a decision. Memoize all possible moves for quick lookup 

      List<string> [,] possibleMoves = new List<string> [8,8];

      bool skipTurn = true;
      for (int col = 0; col < 8; ++col)
      {
         for (int row = 0; row < 8; ++row)
         {
            possibleMoves [col][row] = possibleMoves(col, row, whosTurn);

            if (possibleMoves [col][row].Count > 0)
            {
               skipTurn = false;
            }
         }
      }

      if (skipTurn == true)
      {
         whosTurn = (whosTurn == 'b') ? 'w' : 'b';

         //TODO: tell UI that we're skipping the player's turn
         return;
      }

      
      //Receive input from UI and update board accordingly
      bool successfulMove = false;
      while (!successfulMove)
      {
         successfulMove = updateBoard (whosTurn, possibleMoves [col][row]);
      }

      whosTurn = (whosTurn == 'b') ? 'w' : 'b';
   }


   //Change ownership of the relevant pieces
   bool updateBoard (char whosTurn, List<string> moves)
   {
      if (moves.Count == 0)
      {
         return false;
      }


      if (moves.Contains("N"))
      {
         int i = 1;
         while (board [col][row-i] != whosTurn)
         {
            board [col][row-i] = whosTurn;
            ++i;
         }
         return true;
      }

      if (moves.Contains("S"))
      {
         int i = 1;
         while (board [col][row+i] != whosTurn)
         {
            board [col][row+i] = whosTurn;
            ++i;
         }
         return true;
      }

      if (moves.Contains("E"))
      {
         int i = 1;
         while (board [col+i][row] != whosTurn)
         {
            board [col+i][row] = whosTurn;
            ++i;
         }
         return true;
      }      

      if (moves.Contains("W"))
      {
         int i = 1;
         while (board [col-i][row] != whosTurn)
         {
            board [col-i][row] = whosTurn;
            ++i;
         }
         return true;
      }

      if (moves.Contains("NE"))
      {
         int i = 1;
         while (board [col-i][row+i] != whosTurn)
         {
            board [col-i][row+i] = whosTurn;
            ++i;
         }
            return true;
      }

      if (moves.Contains("NW"))
      {
         int i = 1;
         while (board [col-i][row-i] != whosTurn)
         {
            board [col-i][row-i] = whosTurn;
            ++i;
         }
         return true;
      }

      if (moves.Contains("SE"))
      {
         int i = 1;
         while (board [col+i][row+i] != whosTurn)
         {
            board [col+i][row+i] = whosTurn;
            ++i;
         }
         return true;
      }

      if (moves.Contains("SW"))
      {
         int i = 1;
         while (board [col+i][row-i] != whosTurn)
         {
            board [col+i][row-i] = whosTurn;
            ++i;
         }
         return true;
      }

   }



   //For column and row passed in from UI, determine possible moves
   public List<str>  possibleMoves (int col, int row, char whosTurn)
   {
      string possibleMoves = [];

      if (whosTurn == 'b')
      {
         //check up
         if (row-1 >= 0 && board [col][row-1] == 'w')
         {
            for (int i = 2; (row - i) >= 0; ++i)
            {
               if (board [col][row-i] == 'w')
               {
                  continue;
               }
               else if (board [col][row-i] == 'b')
               {
                  possibleMoves.Add ("N");
                  break;
               }
            }
         }

         //check down
         if (row+1 < 8 && board [col][row+1] == 'w')
         {
            for (int i = 2; (row + i) < 8; ++i)
            {
               if (board [col][row+i] == 'w')
               {
                  continue;
               }
               else if (board [col][row+i] == 'b')
               {
                  possibleMoves.Add ("S");
                  break;
               }
            }
         }

         //check left
         if (col-1 >= 0 && board [col-1][row] == 'w')
         {
            for (int i = 2; (col - i) >= 0; ++i)
            {
               if (board [col-i][row] == 'w')
               {
                  continue;
               }
               else if (board [col-i][row] == 'b')
               {
                  possibleMoves.Add ("W");
                  break;
               }
            }
         }

         //check right
         if (col+1 < 8 && board [col+1][row] == 'w')
         {
            for (int i = 2; (row + i) < 8 ; ++i)
            {
               if (board [col+i][row] == 'w')
               {
                  continue;
               }
               else if (board [col+i][row] == 'b')
               {
                  possibleMoves.Add ("E");
                  break;
               }
            }
         }


         //check NW
         if (col-1 >= 0  && row-1 >= 0 && board [col-1][row-1] == 'w')
         {
            dist = Math.min(row, col);

            for (int i = 2; (dist - i) >= 0; ++i)
            {
               if (board [col-i][row-i] == 'w')
               {
                  continue;
               }
               else if (board [col-i][row-i] == 'b')
               {
                  possibleMoves.Add ("NW");
                  break;
               }
            }
         }

         //check NE

         //check SW
         if (col-1 >= 0 && row+1 < 8 && board [col-1][row+1] == 'w')
         {
            dist = Math.min(col, 8-row);

            for (int i = 2; (dist - i) >= 0; ++i)
            {
               if (board [col-i][row+i] == 'w')
               {
                  continue;
               }
               else if (board [col-i][row+i] == 'b')
               {
                  possibleMoves.Add ("SW");
                  break;
               }
            }
         }

         //check SE
         if (col+1 < 8 && row+1 < 8 && board [col+1][row+1] == 'w')
         {
            dist = Math.max(row, col);

            for (int i = 2; (dist + i) < 8; ++i)
            {
               if (board [col+i][row+i] == 'w')
               {
                  continue;
               }
               else if (board [col+i][row+i] == 'b')
               {
                  possibleMoves.Add ("SE");
                  break;
               }
            }
         }
      }
         

      else if (whosTurn == 'w')
      {
         //check up
         if (row-1 >= 0 && board [col][row-1] == 'b')
         {
            for (int i = 2; (row - i) >= 0; ++i)
            {
               if (board [col][row-i] == 'b')
               {
                  continue;
               }
               else if (board [col][row-i] == 'w')
               {
                  possibleMoves.Add ("N");
                  break;
               }
            }
         }

         //check down
         if (row+1 < 8 && board [col][row+1] == 'b')
         {
            for (int i = 2; (row + i) < 8; ++i)
            {
               if (board [col][row+i] == 'b')
               {
                  continue;
               }
               else if (board [col][row+i] == 'w')
               {
                  possibleMoves.Add ("S");
                  break;
               }
            }
         }

               //check left
               if (col-1 >= 0 && board [col-1][row] == 'b')
               {
                        for (int i = 2; (col - i) >= 0; ++i)
                        {
                                 if (board [col-i][row] == 'b')
                                 {
                                          continue;
                                 }
                                 else if (board [col-i][row] == 'w')
                                 {
                                          possibleMoves.Add ("W");
                                          break;
                                 }
                        }
               }

               //check right
               if (col+1 < 8 && board [col+1][row] == 'b')
               {
                        for (int i = 2; (row + i) < 8 ; ++i)
                        {
                                 if (board [col+i][row] == 'b')
                                 {
                                          continue;
                                 }
                                 else if (board [col+i][row] == 'w')
                                 {
                                          possibleMoves.Add ("E");
                                          break;
                                 }
                        }
               }


               //check NW
               if (col-1 >= 0  && row-1 >= 0 && board [col-1][row-1] == 'b')
               {
                        dist = Math.min(row, col);


                        for (int i = 2; (dist - i) >= 0; ++i)
                        {
                                 if (board [col-i][row-i] == 'b')
                                 {
                                          continue;
                                 }
                                 else if (board [col-i][row-i] == 'w')
                                 {
                                          possibleMoves.Add ("NW");
                                          break;
                                 }
                        }
               }

               //check NE

               //check SW
               if (col-1 >= 0 && row+1 < 8 && board [col-1][row+1] == 'b')
               {
                        dist = Math.min(col, 8-row);

                        for (int i = 2; (dist - i) >= 0; ++i)
                        {
                                 if (board [col-i][row+i] == 'b')
                                 {
                                          continue;
                                 }
                                 else if (board [col-i][row+i] == 'w')
                                 {
                                          possibleMoves.Add ("SW");
                                          break;
                                 }
                        }
               }

               //check SE
               if (col+1 < 8 && row+1 < 8 && board [col+1][row+1] == 'b')
               {
                        dist = Math.max(row, col);

                        for (int i = 2; (dist + i) < 8; ++i)
                        {
                                 if (board [col+i][row+i] == 'b')
                                 {
                                          continue;
                                 }
                                 else if (board [col+i][row+i] == 'w')
                                 {
                                          possibleMoves.Add ("SE");
                                          break;
                                 }
                        }
               }
               
      }

                  return possibleMoves;
         }

}
