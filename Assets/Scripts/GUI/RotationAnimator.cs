using System.Collections;
using UnityEngine;
using RubixCube.Core;
using System;

public class RotationAnimator : MonoBehaviour {
    [SerializeField] private CubeData cubeData;
    [SerializeField] private Transform pivoter;
    public bool animating;
    
    public Coroutine AnimateRotation(Move move, ulong rotationMask, Action callback) {
        animating = true;
        return StartCoroutine(Animate(move, rotationMask, callback));
    }
    
    private IEnumerator Animate(Move move, ulong rotationMask, Action callback) {
        Transform[] originalParents = AssignToPivot(rotationMask);
        
        float duration = SettingsSaveLoad.AnimationDuration;
        if (move.rotationTypeIndex == Move.DoubleRotation)
            duration *= 2;
        
        Vector3 startEuler = Vector3.zero;
        Vector3 targetEuler = GetMoveRotationEuler(move);
        float startTime = Time.time;
        float endTime = startTime + duration;
            
        while (Time.time < endTime) {
            // percent in its decimal form
            float progessProportion = (Time.time - startTime) / duration;
            // use the lerp on the vector class rather than the quaternion class
            // since it rotates the wrong way for double moves for some reason
            Vector3 newEuler = Vector3.Lerp(startEuler, targetEuler, progessProportion);
            // convert the vector to a quaternion using Euler
            pivoter.localRotation = Quaternion.Euler(newEuler);
            yield return null;
        }
        
        // reset the pivot rotation
        pivoter.rotation = Quaternion.Euler(startEuler);
        
        // reassign all facelets to their original parents so we don't rotate them with the next rotation
        AssignToOriginalParents(originalParents, rotationMask);
        animating = false;
        callback();
    }
    
    private Transform[] AssignToPivot(ulong rotationMask) {
        Transform[] originalParents = new Transform[BitboardHelper.GetBitCount(rotationMask)];

        int i = 0;
        while (rotationMask != 0) {
            int bitPos = BitboardHelper.PopLeastSignificantBit(ref rotationMask);
            MeshRenderer facelet = cubeData.facelets[bitPos];
            originalParents[i++] = facelet.transform.parent;
            facelet.transform.SetParent(pivoter);
        }
        
        return originalParents;
    }
    
    private void AssignToOriginalParents(Transform[] originalParents, ulong rotationMask) {
        int i = 0;
        while (rotationMask != 0) {
            int bitPos = BitboardHelper.PopLeastSignificantBit(ref rotationMask);
            Transform facelet = cubeData.facelets[bitPos].transform;
            facelet.transform.SetParent(originalParents[i++]);
        }
    }
    
    private Vector3 GetMoveRotationEuler(Move move) {
        int rotationAmount = 90;
        if (move.rotationTypeIndex == Move.DoubleRotation)
            rotationAmount *= 2;
            
        int rotationMult = move.rotationTypeIndex == Move.CounterClockwiseRotation ? -1 : 1;
        
        switch (move.rotationIndex) {
            case Move.TopFaceRotationIndex:
            case Move.TopWideRotationIndex:
            case Move.YCubeRotationIndex:
                return new Vector3(0, rotationAmount * rotationMult, 0);
            case Move.FrontFaceRotationIndex:
            case Move.FrontWideRotationIndex:
            case Move.SSliceRotationIndex:
            case Move.ZCubeRotationIndex:
                return new Vector3(0, 0, -rotationAmount * rotationMult);
            case Move.RightFaceRotationIndex:
            case Move.RightWideRotationIndex:
            case Move.XCubeRotationIndex:
                return new Vector3(rotationAmount * rotationMult, 0, 0);
            case Move.BackFaceRotationIndex:
            case Move.BackWideRotationIndex:
                return new Vector3(0, 0, rotationAmount * rotationMult);
            case Move.LeftFaceRotationIndex:
            case Move.LeftWideRotationIndex:
            case Move.MSliceRotationIndex:
                return new Vector3(-rotationAmount * rotationMult, 0, 0);
            case Move.BottomFaceRotationIndex:
            case Move.BottomWideRotationIndex:
            case Move.ESliceRotationIndex:
                return new Vector3(0, -rotationAmount * rotationMult, 0);
        }
        
        return Vector3.zero;
    }
}
