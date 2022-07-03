using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests.Saved_test_sudokus;

public record SudokuFixture(string Sudoku, string SolvedSudoku);

public static class TestSudokuFixtures
{
    private static string MediumSudokuText =
        @"752| 4 | 31
           8 |  1|   
             | 2 |5 9
          -----------
            7|   |   
          2 8|   | 5 
          691|75 |   
          -----------
             |9  |   
          92 | 8 | 63
           65|  4| 2 ";

    private static string MediumSudokuSolution =
        @"752|649|831
          489|531|672
          316|827|549
          -----------
          547|218|396
          238|496|157
          691|753|284
          -----------
          873|962|415
          924|185|763
          165|374|928";

    public static SudokuFixture MediumSudoku => new SudokuFixture(MediumSudokuText, MediumSudokuSolution);


    private static string HardSudokuText =
        @" 49|   |  5
             |831|   
             |   |  8
          -----------
             | 9 |81 
             |  7|6  
            1|   |3  
          -----------
          78 |41 |  9
          294|7  |   
          6  |9  |   ";

    private static string HardSudokuSolution =
        @"849|672|135
          572|831|946
          136|549|728
          -----------
          457|396|812
          328|157|694
          961|284|357
          -----------
          783|415|269
          294|763|581
          615|928|473";

    public static SudokuFixture HardSudoku => new SudokuFixture(HardSudokuText, HardSudokuSolution);

    
    private static string ExpertSudokuText =
        @" 4 |  5|87 
             |   |1  
           9 |   |  2
          -----------
             | 7 |4  
           51|3  |  7
            3|  6|   
          -----------
            5|  2| 9 
             |5 8|6  
             | 64|   ";

    private static string ExpertSudokuSolution =
        @"342|915|876
          568|427|139
          197|683|542
          -----------
          926|871|453
          851|349|267
          473|256|918
          -----------
          685|132|794
          734|598|621
          219|764|385";

    public static SudokuFixture ExpertSudoku => new SudokuFixture(ExpertSudokuText, ExpertSudokuSolution);

    private static string Sudoku =
        @"9 1|  5|4  
             |2  | 7 
           8 |   |   
          -----------
          4 6|  9|1  
          3  |   |   
             | 5 |  9
          -----------
          6 8| 7 | 4 
           5 |   |8  
           3 |  6|   ";

    private static string SudokuSolution =
        @"961|735|482
          543|218|976
          287|694|315
          -----------
          476|329|158
          395|861|724
          812|457|639
          -----------
          628|973|541
          759|142|863
          134|586|297";

    public static SudokuFixture EvilSudoku => new SudokuFixture(Sudoku, SudokuSolution);

}

