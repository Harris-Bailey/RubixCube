using UnityEngine;
using RubixCube.Core;
using System.Collections;
using System.Threading.Tasks;

public class CubeVisualiser : MonoBehaviour {
    
    [SerializeField] private CubeData cubeData;
    [SerializeField] private RotationAnimator rotationAnimator;
    [SerializeField] private RectTransform mainUIElements;
    [SerializeField] private RectTransform loadingIcon;
    CubeInputHandler inputHandler;
    Color[] colours;
    Cube cube;
    CFOPSolver solver;
    private bool solving = false;
    private ulong[] bitsRotatedByRotationMask;
    string solutionSequence = string.Empty;

    private void Awake() {
        bitsRotatedByRotationMask = new ulong[RotationData.BitsMovedByRotationMask.Length];
        for (int i = 0; i < RotationData.BitsMovedByRotationMask.Length; i++) {
            ulong centersOmittedThatRotate = GetOmittedRotatingCenters(i);
            bitsRotatedByRotationMask[i] = RotationData.BitsMovedByRotationMask[i] | centersOmittedThatRotate;
        }
        
        cube = new Cube();
        inputHandler = new CubeInputHandler();
        solver = new CFOPSolver();
        
        SettingsSaveLoad.OnAnySettingChanged += UpdateColoursOfCube;
        UpdateColoursOfCube();
    }
    
    private void UpdateColoursOfCube() {
        colours = new Color[] {
            SettingsSaveLoad.CenterOneColour,
            SettingsSaveLoad.CenterTwoColour,
            SettingsSaveLoad.CenterThreeColour,
            SettingsSaveLoad.CenterFourColour,
            SettingsSaveLoad.CenterFiveColour,
            SettingsSaveLoad.CenterSixColour,
        };
        RenderCube();
    }
    
    private ulong GetOmittedRotatingCenters(int rotationIndex) {
        switch (rotationIndex) {
            case Move.TopFaceRotationIndex:
            case Move.TopWideRotationIndex:
                return RotationData.TopCenterFaceletMask;
            case Move.FrontFaceRotationIndex:
            case Move.FrontWideRotationIndex:
                return RotationData.FrontCenterFaceletMask;
            case Move.RightFaceRotationIndex:
            case Move.RightWideRotationIndex:
                return RotationData.RightCenterFaceletMask;
            case Move.BackFaceRotationIndex:
            case Move.BackWideRotationIndex:
                return RotationData.BackCenterFaceletMask;
            case Move.LeftFaceRotationIndex:
            case Move.LeftWideRotationIndex:
                return RotationData.LeftCenterFaceletMask;
            case Move.BottomFaceRotationIndex:
            case Move.BottomWideRotationIndex:
                return RotationData.BottomCenterFaceletMask;
            case Move.XCubeRotationIndex:
                return RotationData.LeftCenterFaceletMask | RotationData.RightCenterFaceletMask;
            case Move.YCubeRotationIndex:
                return RotationData.TopCenterFaceletMask | RotationData.BottomCenterFaceletMask;
            case Move.ZCubeRotationIndex:
                return RotationData.FrontCenterFaceletMask | RotationData.BackCenterFaceletMask;
            default:
                return 0;
        };
    }

    private void Update() {
        if (solving || rotationAnimator.animating)
            return;

        HandleRotation();
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            
        }
    }
    
    public void Solve() {
        solving = true;
        mainUIElements.gameObject.SetActive(false);
        Task.Run(() => {
            CubeMask preSolvedState = new CubeMask(cube.GetState());
            solutionSequence = solver.Solve(cube);
            solving = false;
            cube.SetState(preSolvedState.ColouredFaceletBitboards);
        });
        StartCoroutine(ShowProcessingIcon());
    }
    
    public void Scramble() {
        Move[] moves = Move.GetPureRandomMoves(100);        
        StartCoroutine(AnimateSequence(moves, cube));
    }
    
    public void ResetCube() {
        cube = new Cube();
        RenderCube();
    }
    
    IEnumerator ShowProcessingIcon() {
        loadingIcon.gameObject.SetActive(true);
        while (solving) {
            yield return null;
        }
        loadingIcon.gameObject.SetActive(false);
        mainUIElements.gameObject.SetActive(true);
        solutionSequence = Move.CleanSequence(solutionSequence);
        Debug.Log(Move.GetMovesFromSequence(solutionSequence).Length);
        yield return AnimateSequence(Move.GetMovesFromSequence(solutionSequence), cube);
    }
    
    IEnumerator AnimateSequence(Move[] moves, Cube cube) {
        foreach (Move move in moves) {
            cube.Rotate(move);
            yield return rotationAnimator.AnimateRotation(move, bitsRotatedByRotationMask[move.rotationIndex], RenderCube);
        }
        RenderCube();
    }
    
    private void HandleRotation() {        
        Move inputtedMove = inputHandler.GetMoveFromInput();
        
        if (inputtedMove.IsNullMove)
            return;
            
        cube.Rotate(inputtedMove);
        rotationAnimator.AnimateRotation(inputtedMove, bitsRotatedByRotationMask[inputtedMove.rotationIndex], RenderCube);
    }
    
    public void RenderCube() {
        for (int i = 0; i < cube.BitboardsMatchingCenters.Length; i++) {
            ulong bitboard = cube.BitboardsMatchingCenters[i];
            while (bitboard != 0) {
                int pos = BitboardHelper.PopLeastSignificantBit(ref bitboard);
                cubeData.facelets[pos].material.color = colours[i];
            }
        }
    }
}
