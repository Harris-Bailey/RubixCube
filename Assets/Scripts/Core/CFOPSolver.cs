using System.IO;
using System.Linq;

namespace RubixCube.Core {

    public class CFOPSolver {    
        // sequences courtesy of https://speedcubedb.com/a/3x3/
        private static readonly string[] f2lMoveSequences = {
            #region Basic F2L
            
            //f2l 1
            "U R U' R'",
            "F' r U r'",
            "U L U' L'",
            "U f R' f'",
            
            // f2l 2
            "F R' F' R",
            "U' L' U L",
            "l U L' U' M'",
            "U' R' U R",
            
            // f2l 3
            "F' U' F",
            "L' U' L",
            "y R' U' R",
            "R' U' R",
            
            // f2l 4
            "R U R'",
            "F U F'",
            "L U L'",
            "f R f'",
            
            // f2l 5
            "U' R U R' U2 R U' R'",
            "U R' F r U' r' F' R",
            "U' L U L' U2 L U' L'",
            "U' R' F R U R' U' F' R",
            
            // f2l 6
            "U' r U' R' U R U r'",
            "U L' U' L U2 L' U L",
            "U r U' r' U' L U F L'",
            "U R' U' R U2 R' U R",
            
            // f2l 7
            "M' U' M U2 r U' r'",
            "F U R U2 R' U F'",
            "U' L U2 L' U2 L U' L'",
            "r U2 R2 U' R2 U' r'",
            
            // f2l 8
            "d R' U2 R U R' U2 R",
            "U L' U2 L U L' U2 L",
            "l' U2 L2 U L2 U l",
            "U R' U2 R U R' U2 R",
            
            // f2l 9
            "U' R U' R' U F' U' F",
            "U L' U' L U' L' U' L",
            "y U R' U' R U' R' U' R",
            "U R' U' R U' R' U' R",
            
            // f2l 10
            "U' R U R' U R U R'",
            "U L' U L U' F U F'",
            "U' L U L' U L U L'",
            "U R' U R U' f R f'",
            
            // f2l 11
            "y' R U2 R2 U' R2 U' R'",
            "L' U L U' L' U L U2 L' U L",
            "U' L U2 L' U f' L' f",
            "R' U R U' R' U R U2 R' U R",
            
            // f2l 12
            "R U' R' U R U' R' U2 R U' R'",
            "U L' U2 L U' F U F'",
            "L' U2 L2 U L2 U L",
            "U R' U2 R U' f R f'",
            
            // f2l 13
            "y' U R' U R U' R' U' R",
            "U L' U L U' L' U' L",
            "d L' U L U' L' U' L",
            "U R' U R U' R' U' R",
            
            // f2l 14
            "U' R U' R' U R U R'",
            "d' L U' L' U L U L'",
            "U' L U' L' U L U L'",
            "y U' R U' R' U R U R'",
            
            // f2l 15
            "R' D' R U' R' D R U R U' R'",
            "L' U L U2 F U F'",
            "L U L' U2 L U' L' U L U' L'",
            "R' U R U2 f R f'",
            
            // f2l 16
            "R U' R' U2 F' U' F",
            "F U' R U' R' U2 F'",
            "L U' L' U2 f' L' f",
            "R' U' R U2 R' U R U' R' U R",
            
            // f2l 17
            "R U2 R' U' R U R'",
            "y L U2 L' U' L U L'",
            "L U2 L' U' L U L'",
            "y' L U2 L' U' L U L'",
            
            // f2l 18
            "y' R' U2 R U R' U' R",
            "L' U2 L U L' U' L",
            "y R' U2 R U R' U' R",
            "R' U2 R U R' U' R",
            
            // f2l 19
            "U R U2 R' U R U' R'",
            "U L' U L2 F' L' F L' U L",
            "U L U2 L' U L U' L'",
            "y U R U2 R' U R U' R'",
            
            // f2l 20
            "y' U' R' U2 R U' R' U R",
            "U' L' U2 L U' L' U L",
            "y U' R' U2 R U' R' U R",
            "U' R' U2 R U' R' U R",
            
            // f2l 21
            "U2 R U R' U R U' R'",
            "l' U l U2 l' U' l",
            "L U' L' U2 L U L'",
            "r' U r U2 r' U' r",
            
            // f2l 22
            "r U' r' U2 r U r'",
            "L' U L U2 L' U' L",
            "l U' l' U2 l U l'",
            "R' U R U2 R' U' R",
            
            // f2l 23
            "U R U' R' U' R U' R' U R U' R'",
            "F' U' L' U L F L' U L",
            "U L U' L' U' L U' L' U L U' L'",
            "U R' F R' F' R2 U' R' U R",
            
            // f2l 24
            "F U R U' R' F' R U' R'",
            "U' L' U L U L' U L U' L' U L",
            "U2 r U R' U R U2 B r'",
            "R' U' R U2 R' U' R U R' U' R",
            
            // f2l 25
            "U' R' F R F' R U R'",
            "U' L' U L F' r U r'",
            "R D' R' U' R D R' L U L'",
            "U' R' U M U' R U M'",
            
            // f2l 26
            "U R U' R' F R' F' R",
            "r U r' U' r' F r F'",
            "L S L' U L S' L'",
            "R' U R U R' U R U' R' U' R",
            
            // f2l 27
            "R U' R' U R U' R'",
            "L' U' L U F' r U r'",
            "L U' L' U L U' L'",
            "R' U2 R' F R F' R",
            
            // f2l 28
            "R U R' U' F R' F' R",
            "L' U L U' L' U L",
            "L U2 L F' L' F L'",
            "R' U R U' R' U R",
            
            // f2l 29
            "R' F R F' U R U' R'",
            "L' U' L U L' U' L",
            "y R' U' R U R' U' R",
            "R' U' R U R' U' R",
            
            // f2l 30
            "R U R' U' R U R'",
            "L F' L' F U' L' U L",
            "L U L' U' L U L'",
            "y' L U L' U' L U L'",
            
            // f2l 31
            "U' R' F R F' R U' R'",
            "U L F' L' F L' U L",
            "L U' L F' L' F L'",
            "R' U R' F R F' R",
            
            // f2l 32
            "U R U' R' U R U' R' U R U' R'",
            "U' L' U L U' L' U L U' L' U L",
            "L U L' U' L U L' U' L U L'",
            "U' R' U R U' R' U R U' R' U R",
            
            // f2l 33
            "U' R U' R' U2 R U' R'",
            "R' D R U' R' D' R",
            "U' L U' L' U2 L U' L'",
            "U' R D R' U R D' R'",
            
            // f2l 34
            "U R U R' U2 R U R'",
            "U L' U L U2 L' U L",
            "U L U L' U2 L U L'",
            "U R' U R U R' U2 R",
            
            // f2l 35
            "U' R U R' U F' U' F",
            "U2 F U F' U' L' U L",
            "U' L U L' U f' L' f",
            "U' f R f' U R' U' R",
            
            // f2l 36
            "U F' U' F U' R U R'",
            "U L' U' L d' L U L'",
            "U f' L' f U' L U L'",
            "U2 R' U' R U R' F' U' F R",
            
            // f2l 37
            "R2 U2 F R2 F' U2 R' U R'",
            "L2 U2 F' L2 F U2 L U' L",
            "L U' L' l' U2 L2 U L2 U l",
            "R' U R r U2 R2 U' R2 U' r'",
            
            // f2l 38
            "R U' R' U' R U R' U2 R U' R'",
            "L' U L U' L' U2 L U' L' U L",
            "L U L' U' L U2 L' U' L U L'",
            "R' U' R U2 R' U R U' R' U' R",
            
            // f2l 39
            "R U' R' U R U2 R' U R U' R'",
            "L' U' L U L' U2 L U L' U' L",
            "L U L' U2 L U' L' U L U L'",
            "R' U' R U R' U2 R U R' U' R",
            
            // f2l 40
            "r U' r' U2 r U r' R U R'",
            "L' U L F R U2 R' F'",
            "l U' l' U2 l U l' L U L'",
            "R' U R r' U r U2 r' U' r",
            
            // f2l 41
            "R U' R' r U' r' U2 r U r'",
            "l' U l U2 l' U' l L' U' L",
            "f' L f U' L U L' U L U L'",
            "r' U r U2 r' U' r R' U' R",
            
            #endregion
            #region Advanced F2L
            
            // af2l 1
            "S R' S'",
            "L' U' L U R U' R'",
            "S' L' S",
            "R' U' R U L U' L'",
            
            // af2l 2
            "L F' U2 F L'",
            "L' U' L y R U' R'",
            "L U' L' U' R U' R'",
            "F R' U2 R F'",
            
            // af2l 3
            "R U' R' U F' r U r'",
            "L' U' L2 U2 L'",
            "y R' U' R U' R U' R'",
            "R' U' R2 U2 R'",
            
            // af2l 4
            "F R' F' R U R' U2 R",
            "U2 L' U L U' R U R'",
            "B L' B' L U L' U2 L",
            "U2 R' U R U' L U L'",
            
            // af2l 5
            "R U R' U L U L'",
            "L F' L' F R' U2 R",
            "L U L' U R U R'",
            "U2 R' U R U2 F U F'",
            
            // af2l 6
            "R U R' F U F'",
            "U2 L' U L U L U L'",
            "L U L' f R f'",
            "U2 R' U R U R U R'",
            
            // af2l 7
            "U' F' U' f R S'",
            "U' L' U L R U' R'",
            "U' f' r' D r S",
            "U' R' U R L U' L'",
            
            // af2l 8
            "U' F' U2 L U L' F",
            "U L' U2 L f R f'",
            "R D' R' U R U D R'",
            "U' R' U R U2 F' r U r'",
            
            // af2l 9
            "y' U R' U2 R U' R U R'",
            "U' L' U L U' L U2 L'",
            "U L' B' L B' L' B L",
            "U R' U2 R U' R U R'",
            
            // af2l 10
            "U' R U R2 U' R",
            "U' F U F2 U' F",
            "U' L U L2 U' L",
            "R' U R L U' L' U L U L'",
            
            // af2l 11
            "U' R U' R' L U' L'",
            "U L' U2 L U R' U' R",
            "U' L U' L' R U' R'",
            "U' R' U' R L' U' L",
            
            // af2l 12
            "U2 R U R' L' U L",
            "U' L U L2 U' L2 U L'",
            "U' L U L' U2 R' U' R",
            "U' R S' U2 S R'",
            
            // af2l 13
            "U R' F R F' R' U' R",
            "U' L F' L2 U L U2 F",
            "U r' U L U' x L' U' L",
            "y U R' F R F' R' U' R",
            
            // af2l 14
            "U2 r2 U B2 U' r2",
            "U2 R2 D' F2 D R2",
            "U2 R2 D B2 D' R2",
            "U2 L2 D' B2 D L2",
            
            // af2l 15
            "U R' F R2 U' R' U2 F'",
            "U' L F' L' F L U L'",
            "y' U' L F' L' F L U L'",
            "U' R' U R U' S R S'",
            
            // af2l 16
            "U' F' R' U' R F",
            "U' R' D' F' D R",
            "d' L' u' L' u L",
            "U' R' U' R U f' L' f",
            
            // af2l 17
            "U2 R B' U' B R'",
            "U2 F R' U' R F'",
            "U2 R d' R' U R F'",
            "U2 f R' f' U L' U' L",
            
            // af2l 18
            "U F' U2 F L' U' L",
            "U L' U2 L f' L' f",
            "U' L U' L' R2 F R F' R",
            "U R' U2 R F' U' F",
            
            // af2l 19
            "U' R U2 R' f R f'",
            "U' F U2 F' R U R'",
            "U' L U2 L' F U F'",
            "U R' U R U f' L f",
            
            // af2l 20
            "U R U R' L U L'",
            "U L' U L R' U R",
            "U L U L' R U R'",
            "U R' U R L' U L",
            
            // af2l 21
            "U R F U F' R'",
            "U L' U L R B L' B' M' x'",
            "U L U L' U' f R f'",
            "U F D R D' F'",
            
            // af2l 22
            "R U' R2 U R",
            "R L' U2 L R'",
            "L U' L2 U L",
            "L R' U2 R L'",
            
            // af2l 23
            "L F' U F L'",
            "R' F U' F' R",
            "F' L U' L' F",
            "F R' U R F'",
            
            // af2l 24
            "R U' R' U2 L' U L",
            "L' U L2 U' L'",
            "L U' L' U2 R' U R",
            "R' U R2 U' R'",
            
            // af2l 25
            "F' U R' U2 R F",
            "R' F R2 U' R2 F' R",
            "L U L' y' U' R U' R'",
            "y R U' R' U' f R f'",
            
            // af2l 26
            "L R U2 R' L'",
            "L' U' L U R' U2 R",
            "L U L' U' R U2 R'",
            "R' U' R U2 L' U L",
            
            // af2l 27
            "R' F R2 U R' F'",
            "L' U L U f' L' f",
            "L U L' U y R U' R'",
            "F' R' U2 R U' F",
            
            // af2l 28
            "R U' R2 U' R U' R' U' R",
            "L' U L U R U R' U R U' R'",
            "L U' L2 U' L U' L' U' L",
            "R' U R U L U L' U L U' L'",
            
            // af2l 29
            "F' U L' U L U' L U L' F",
            "L' U L y' L U L' U L U' L'",
            "f' L f R U R2 F R F'",
            "R' U R F R' F R F2",
            
            // af2l 30
            "R U' R' U' R U' R' U2 L' U' L",
            "L' U L U' L U L' U L U' L'",
            "L U' L' U' R' U R U R' U' R",
            "R' U R U' R U R' U R U' R'",
            
            // af2l 31
            "R U' R' U R' U' R U' R' U R",
            "R' F R U' R' F' R2 U R' U R U R'",
            "L U' L' U L' U' L U' L' U L",
            "R' U R U L U' L' U' L U L'",
            
            // af2l 32
            "R U R' U' R U' R' f' L' f",
            "F U' R U' R' U R' U' R F'",
            "L U' L' y L' U' L U' L' U L",
            "R' U R U' L' U L d' L U L'",
            
            // af2l 33
            "R U' R' U' L' U' L U' L' U L",
            "L' U L2 U L' U L U L'",
            "L U' L' U' R' U' R U' R' U R",
            "R' U R U R' U R2 U R'",
            
            // af2l 34
            "F' R' U R U' R' U' R F",
            "L' U' L U y' R' U R U' R' U' R",
            "L U L' y U2 L U2 L' U' L U L'",
            "R' U' R y U R' U R U' R' U' R",
            
            // af2l 35
            "R U' R' U' R U R' L U' L'",
            "L' U' L R' U R U' R' U' R",
            "L U' L' U' L U L' R U' R'",
            "R' U R U L' U2 L U' L' U L",
            
            // af2l 36
            "R' F R2 U' R' U' R U R' U2 F'",
            "L' U' L d' R' U R U' R' U' R",
            "L U' L' U' L U L' y' L U2 L'",
            "R' U2 R y' R' U R U' R' U' R",
            
            // af2l 37
            "R U R' U2 R U' R' f R f'",
            "L' U' L y' R' U2 R U R' U' R",
            "L U' L' y L U2 L' U L U' L'",
            "R' U R U R' U' R y R' U2 R",
            
            // af2l 38
            "R U' R' U R U2 R' L U2 L'",
            "L' U L U L' U' L R' U R",
            "L U' L' U' R U2 R' U R U' R'",
            "R' U' R U' L' U2 L U L' U' L",
            
            // af2l 39
            "R U R' d' L U' L' U L U L'",
            "F L U' L' U L U L' F'",
            "L U' L' y' U2 L U2 L' U L U' L'",
            "R' U' R y' U2 R' U2 R U R' U' R",
            
            // af2l 40
            "R U' R2 U2 R U R' U2 R",
            "F2 R' F2 D' R U' R' D R",
            "L U' L2 U2 L U2 L' U L",
            "R' U R U2 L U2 L' U2 L U' L'",
            
            // af2l 41
            "R U' R' d R' U2 R L' U L",
            "F U2 R U2 R2 U' R F'",
            "L U L' R U2 R' y' U R' U' R",
            "R' U R y' U R U2 R' U2 R U' R'",
            
            // af2l 42
            "R U' R' U2 L' U2 L U2 L' U L",
            "L' U L2 U2 L' U2 L U' L'",
            "L U L' R U2 R2 U' R2 U' R'",
            "R' U R2 U2 R' U R' F R F'",
            
            // af2l 1a
            "R' F R S R' f'",
            "L' U' L F' U' F",
            "f' L' f L' U' L",
            "R' U' R f' L' f",
            
            // af2l 2a
            "R' F R F' L U2 L'",
            "L' U' L U' R' U' R",
            "L' B L B' R U2 R'",
            "R' U' R U' L' U' L",
            
            // af2l 3a
            "R' F R2 U' R' U F'",
            "F' L F L' U2 L U' L'",
            "f' L' f U2 R' U' R",
            "B' R B R' U2 R U' R'",
            
            // af2l 4a
            "U R U R2 U2 R",
            "U L' U L y' U2 R' U R",
            "U L U L2 U2 L",
            "U R' U R y U2 R' U R",
            
            // af2l 5a
            "U l U' F2 U l'",
            "U L' U L R' U2 R",
            "y' U L' U L R' U2 R",
            "U R' U R L' U2 L",
            
            // af2l 6a
            "U R L' U L R'",
            "U L' U L f' L f",
            "U L R' U R L'",
            "U S R S'",
            
            // af2l 7a
            "R U2 R' U R' U' R",
            "y' U2 R U' R' U2 R' U R",
            "L U2 L' U L' U' L",
            "R B R' B R B' R'",
            
            // af2l 8a
            "U2 R U' R' d L' U L",
            "U2 L F' L' F U2 R' U' R",
            "U2 L U' L' y L' U2 L",
            "U L' D L U' L' U' D' L",
            
            // af2l 9a
            "U2 R U' R' L' U L",
            "y U2 L U' L' R' U R",
            "U2 L U' L' R' U R",
            "y U2 R U' R' L' U L",
            
            // af2l 10a
            "U' R U' R' U f R' f'",
            "y' U' F' R' U' R F",
            "U' F' D' L' D F",
            "y U' F' R' U' R F",
            
            // af2l 11a
            "U2 L U' F' U F L'",
            "U' F U F' U' R' U' R",
            "U' L U L' U' F' U' F",
            "U' f R f' U' L' U' L",
            
            // af2l 12a
            "U' R U' R' y L U2 L'",
            "U L' U2 L y' L' U' L",
            "U' L U' L' y' L U2 L'",
            "U R' U2 R y' R' U' R"
            
            #endregion
        };
        private static readonly string[] ollMoveSequences = {
            "R U2 R2 F R F' U2 R' F R F'",
            "y' R U' R2 D' r U r' D R2 U R'",
            "y R' F2 R2 U2 R' F R U2 R2 F2 R",
            "y' R' F2 R2 U2 R' F' R U2 R2 F2 R",
            "r' U2 R U R' U r",
            "r U2 R' U' R U' r'",
            "r U R' U R U2 r'",
            "y2 r' U' R U' R' U2 r",
            "y R U R' U' R' F R2 U R' U' F'",
            "R U R' U R' F R F' R U2 R'",
            "r' R2 U R' U R U2 R' U M'",
            "y' M' R' U' R U' R' U2 R U' M",
            "F U R U2 R' U' R U R' F'",
            "R' F R U R' F' R F U' F'",
            "r' U' r R' U' R U r' U r",
            "r U r' R U R' U' r U' r'",
            "y2 F R' F' R U S' R U' R' S",
            "y R U2 R2 F R F' U2 M' U R U' r'",
            "y S' R U R' S U' R' F R F'",
            "r U R' U' M2 U R U' R' U' M'",
            "R U R' U R U' R' U R U2 R'",
            "R U2 R2 U' R2 U' R2 U2 R",
            "R2 D R' U2 R D' R' U2 R'",
            "r U R' U' r' F R F'",
            "y F' r U R' U' r' F R",
            "y R U2 R' U' R U' R'",
            "R U R' U R U2 R'",
            "r U R' U' M U R U' R'",
            "r2 D' r U r' D r2 U' r' U' r",
            "y' r' D' r U' r' D r2 U' r' U r U r'",
            "R' U' F U R U' R' F' R",
            "S R U R' U' R' F R f'",
            "R U R' U' R' F R F'",
            "y f R f' U' r' U' R U M'",
            "R U2 R2 F R F' R U2 R'",
            "y2 L' U' L U' L' U L U L F' L' F",
            "F R' F' R U R U' R'",
            "R U R' U R U' R' U' R' F R F'",
            "y' f' r U r' U' r' F r S",
            "y R' F R U R' U' F' U R",
            "y2 R U R' U R U2 R' F R U R' U' F'",
            "R' U' R U' R' U2 R F R U R' U' F'",
            "y R' U' F' U F R",
            "y2 F U R U' R' F'",
            "F R U R' U' F'",
            "R' U' R' F R F' U R",
            "F' L' U' L U L' U' L U F",
            "F R U R' U' R U R' U' F'",
            "y2 r U' r2 U r2 U r2 U' r",
            "r' U r2 U' r2 U' r2 U r'",
            "y2 F U R U' R' U R U' R' F'",
            "y2 R' F' U' F U' R U R' U R",
            "r' U' R U' R' U R U' R' U2 r",
            "r U R' U R U' R' U R U2 r'",
            "y R' F U R U' R2 F' R2 U R' U' R",
            "r U r' U R U' R' U R U' R' r U' r'",
            "R U R' U' M' U R U' r'"
        };
        private static readonly string[] pllMoveSequences = {
            "x R' U R' D2 R U' R' D2 R2 x'",
            "x R2 D2 R U R' D2 R U' R x'",
            "y x' R U' R' D R U R' D' R U R' D R U' R' D' x",
            "y R' U' F' R U R' U' R' F R2 U' R' U' R U R' U R",
            "R2 U R' U R' U' R U' R2 D U' R' U R D'",
            "R' U' R U D' R2 U R' U R U' R U' R2 D",
            "R2 U' R U' R U R' U R2 D' U R U' R' D",
            "R U R' U' D R2 U' R U' R' U R' U R2 D'",
            "M2 U' M2 U2 M2 U' M2",
            "y2 x R2 F R F' R U2 r' U r U2 x'",
            "R U R' F' R U R' U' R' F R2 U' R'",
            "R U R' U R U R' F' R U R' U' R' F R2 U' R' U2 R U' R'",
            "r' D' F r U' r' F' D r2 U r' U' r' F r F'",
            "y R U' R' U' R U R D R' U' R D' R' U2 R'",
            "R' U2 R U2 R' F R U R' U' R' F' R2",
            "R U R' U' R' F R2 U' R' U' R U R' F'",
            "y2 M2 U M U2 M' U M2",
            "y2 M2 U' M U2 M' U' M2",
            "R' U R' U' R D' R' D R' U D' R2 U' R2 D R2",
            "F R U' R' U' R U R' F' R U R' U' R' F R F'",
            "M' U' M2 U' M2 U' M' U2 M2"
        };
        
        private static readonly string[] lastLayerMoveSequences;
        
        static CFOPSolver() {
            // this file contains all the last layer algorithms provided in https://docs.google.com/spreadsheets/d/1TkCEyg4TJRM_4KJv1lPDctvSiSAtUNMm/
            string lastLayerRelativePath = "Assets/Scripts/Core/LastLayerAlgorithms.txt";
            lastLayerMoveSequences = File.ReadLines(lastLayerRelativePath).ToArray();
        }
        
        public string Solve(Cube cube) {
            string crossSolution = SolveCross(cube);
            string f2lSolution = SolveF2L(cube);
            // string ollSolution = SolveOLL(cube);
            // string pllSolution = SolvePLL(cube);
            string lastLayerSolution = SolveLastLayer(cube);
            
            return $"{crossSolution} {f2lSolution} {lastLayerSolution}";
        }
        
        private string SolveCross(Cube cube) {
            // using the iterative deepening to solve the cross
            CubeMask crossSolvedMask = new CubeMask(cube.BitboardsMatchingCenters, 0, 0b010000000ul << 9, 0b010000000ul << 18, 0b010000000ul << 27, 0b010000000ul << 36, 0b010101010ul << 45);
            IterativeDeepening searcher = new IterativeDeepening(cube);
            Move[] movesToSolveCross = searcher.Run(crossSolvedMask, 8);
            
            // performing the moves to solve the cross
            string solution = string.Empty;
            foreach (Move move in movesToSolveCross) {
                cube.Rotate(move);
                solution += $"{move} ";
            }
            return solution.TrimEnd();
        }
        
        private string SolveF2L(Cube cube) { 
            int totalPairsFound = 0;
            
            string solution = string.Empty;
            CubeMask f2lSolvedMask  = new CubeMask(cube.BitboardsMatchingCenters, 0, 0b111111000ul << 9, 0b111111000ul << 18, 0b111111000ul << 27, 0b111111000ul << 36, 0b111111111ul << 45);
            
            // loop until 4 pairs are found or until the cap reaches 0, at which point
            // it's safe to say that the remaining pairs will never be found
            while (totalPairsFound < 4) {
                if (cube.MaskMatchesCubeState(f2lSolvedMask))
                    return solution;
                
                string aufBefore = string.Empty;
                string sequence = string.Empty;
                // need to loop 4 times to rotate the top face due to potential misalignment in the top layer
                for (int j = 0; j < 4; j++) {
                    CubeMask originalState = new CubeMask(cube.GetState());
                    foreach (string moveSequence in f2lMoveSequences) {
                        Move[] moves = Move.GetMovesFromSequence(moveSequence);
                        
                        foreach (Move move in moves) {
                            cube.Rotate(move);
                        }
                        
                        CubeMask frontRightPair = new CubeMask(cube.BitboardsMatchingCenters, 0, 0b100100000ul << 9, 0b001001000ul << 18, 0 << 27, 0 << 36, 0b000000100ul << 45);
                        CubeMask backRightPair  = new CubeMask(cube.BitboardsMatchingCenters, 0, 0 << 9, 0b100100000ul << 18, 0b001001000ul << 27, 0 << 36, 0b100000000ul << 45);
                        CubeMask backLeftPair   = new CubeMask(cube.BitboardsMatchingCenters, 0, 0 << 9, 0 << 18, 0b100100000ul << 27, 0b001001000ul << 36, 0b001000000ul << 45);
                        CubeMask frontLeftPair  = new CubeMask(cube.BitboardsMatchingCenters, 0, 0b001001000ul << 9, 0 << 18, 0 << 27, 0b100100000ul << 36, 0b000000001ul << 45);
                        
                        int numPairsFoundWithMove = 0;
                        if (cube.MaskMatchesCubeState(frontRightPair))
                            numPairsFoundWithMove++;
                        if (cube.MaskMatchesCubeState(backRightPair))
                            numPairsFoundWithMove++;
                        if (cube.MaskMatchesCubeState(backLeftPair))
                            numPairsFoundWithMove++;
                        if (cube.MaskMatchesCubeState(frontLeftPair))
                            numPairsFoundWithMove++;
                        
                        if (numPairsFoundWithMove > totalPairsFound) {
                            // Console.WriteLine(moveSequence + " : " + moveSequence.Length);
                            sequence = $"{aufBefore}{moveSequence} ";
                            totalPairsFound = numPairsFoundWithMove;
                            break;
                        }
                        
                        // if the mask doesn't match the cube state, reset to the original state before performing the f2l sequence
                        cube.SetState(originalState.ColouredFaceletBitboards);
                    }
                    // found the sequence to solve the pair
                    if (sequence != string.Empty)
                        break;
                    
                    // otherwise auf the top layer to see if we can find another solution
                    aufBefore += "U ";
                    cube.Rotate(new Move("U"));
                }
                // there is no f2l sequence found so rotate the cube to see if that provides a solution
                if (sequence == string.Empty) {
                    solution += "y ";
                    cube.Rotate(new Move("y"));
                    continue;
                }
                solution += $"{sequence.Trim()} ";
            }
            return solution.TrimEnd();
        }
        
        private string SolveOLL(Cube cube) {
            CubeMask solvedOLLMask = new CubeMask(cube.BitboardsMatchingCenters, 0b111111111ul, 0, 0, 0, 0, 0);
            string aufBefore = string.Empty;
            
            // loop 4 times to properly align the top layer for the sequence
            for (int i = 0; i < 4; i++) {
                // storing the state before the OLL sequence
                CubeMask state = new CubeMask(cube.GetState());
                
                foreach (string sequence in ollMoveSequences) {
                    Move[] moves = Move.GetMovesFromSequence(sequence);
                    
                    // perform all moves in the sequence
                    foreach (Move move in moves)
                        cube.Rotate(move);
                        
                    // what about when doing an r move and an m move, these cancel each other out
                    // Move[] restorationMoves = RestoreOriginalOrientation(cube, moves);
                    
                    // check whether the mask matches the cube state
                    if (cube.MaskMatchesCubeState(solvedOLLMask)) {
                        // foreach (Move move in Move.GetInverseMoves(restorationMoves))
                        //     cube.Rotate(move);
                        
                        return $"{aufBefore}{sequence}";
                    }
                    
                    // otherwise reset the state
                    cube.SetState(state.ColouredFaceletBitboards);
                }
                
                // auf in order to find the OLL sequence
                cube.Rotate(new Move("U"));
                aufBefore += "U ";
            }
            
            return string.Empty;
        }
        
        private string SolvePLL(Cube cube) {        
            string aufBefore = string.Empty;
            // loop 4 times to properly align the top layer for the sequence
            for (int i = 0; i < 4; i++) {
                // storing the state before performing the PLL sequence
                CubeMask state = new CubeMask(cube.GetState());
                foreach (string sequence in pllMoveSequences) {
                    Move[] moves = Move.GetMovesFromSequence(sequence);
                    
                    // perform all moves in the sequence
                    foreach (Move move in moves)
                        cube.Rotate(move);
                        
                    // need this in the loop because the centers may have changed their positions
                    // in which case doing this before the loop would check the mask with the wrong faces
                    CubeMask solvedPLLMask = new CubeMask(
                        cube.BitboardsMatchingCenters,
                        0b111111111ul,
                        0b111111111ul << 9,
                        0b111111111ul << 18,
                        0b111111111ul << 27,
                        0b111111111ul << 36,
                        0b111111111ul << 45
                    );
                    
                    // since the top layer may be misaligned after the algorithm
                    // perform 4 "U" moves to see if that resolves the cube to the solved state
                    string aufAfter = " ";
                    for (int j = 1; j <= 4; j++) {
                        if (cube.MaskMatchesCubeState(solvedPLLMask)) {
                            // foreach (Move move in Move.GetInverseMoves(restorationMoves))
                            //     cube.Rotate(move);
                            return $"{aufBefore}{sequence}{aufAfter.TrimEnd()}";
                        }
                        // auf move to see if the top layer is misaligned
                        aufAfter += "U ";
                        cube.Rotate(new Move("U"));
                    }
                    
                    // if the cube isn't resolved to the solved state after the auf, this isn't the right PLL sequence
                    // so reset the state and move on to the next sequence
                    cube.SetState(state.ColouredFaceletBitboards);
                }
                
                // auf to find the PLL sequence since the top layer must be misaligned
                aufBefore += "U ";
                cube.Rotate(new Move("U"));
            }
            
            return string.Empty;
        }
        
        // this is the same as the PLL function, just using the new 1 look last layer sequences
        private string SolveLastLayer(Cube cube) {
            string aufBefore = "";
            
            for (int i = 0; i < 4; i++) {

                CubeMask state = new CubeMask(cube.GetState());
                foreach (string sequence in lastLayerMoveSequences) {
                    Move[] moves = Move.GetMovesFromSequence(sequence);
                    foreach (Move move in moves)
                        cube.Rotate(move);
                        
                    CubeMask solvedLastLayerMask = new CubeMask(
                        cube.BitboardsMatchingCenters,
                        0b111111111ul,
                        0b111111111ul << 9,
                        0b111111111ul << 18,
                        0b111111111ul << 27,
                        0b111111111ul << 36,
                        0b111111111ul << 45
                    );
                    
                    string aufAfter = " ";
                    for (int j = 1; j <= 4; j++) {
                        if (cube.MaskMatchesCubeState(solvedLastLayerMask))
                            return $"{aufBefore}{sequence}{aufAfter.TrimEnd()}";

                        aufAfter += "U ";
                        cube.Rotate(new Move("U"));
                    }
                    
                    cube.SetState(state.ColouredFaceletBitboards);
                }
                
                aufBefore += "U ";
                cube.Rotate(new Move("U"));
            }
            return string.Empty;
        }
    }
}

/*

cross:
D F R D' R'
D F R D' R'

f2l:
U U U R U R' U' R U R' R' U R' F R F' R y y y U M' U' M U2 r U' r' U' L' U L F' r U r'
U U U R U R' U' R U R' R' U R' F R F' R y y y U M' U' M U2 r U' r' U' L' U L F' r U r'

oll:
U U U r U r' R U R' U' r U' r'
U U U r U r' R U R' U' r U' r'

pll:
U U R U R' F' R U R' U' R' F R2 U' R' U
U U R U R' F' R U R' U' R' F R2 U' R' U

*/