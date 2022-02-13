// Copyright 2021 Niantic, Inc. All Rights Reserved.

using UnityEngine;

namespace Niantic.ARDK.AR
{
  /// A utility class used to convert to and from raw arrays and matrices.
  internal static class _Convert
  {
    /// Converts a flat, column-major, float array to a Matrix4x4
    internal static Matrix4x4 InternalToMatrix4x4(float[] internalArray)
    {
      var matrix4x4 = Matrix4x4.zero;

      for (int col = 0; col < 4; col++)
      {
        for (int row = 0; row < 4; row++)
        {
          // internalArray is column-major.
          matrix4x4[row, col] = internalArray[row + (col * 4)];
        }
      }

      return matrix4x4;
    }

    /// Converts a flat, column-major, 9-element, float array to a Matrix4x4
    internal static Matrix4x4 Internal3x3ToMatrix4x4(float[] internalArray)
    {
      var matrix4x4 = Matrix4x4.identity;

      matrix4x4[0, 0] = internalArray[0];
      matrix4x4[1, 0] = internalArray[1];
      matrix4x4[3, 0] = internalArray[2];

      matrix4x4[0, 1] = internalArray[3];
      matrix4x4[1, 1] = internalArray[4];
      matrix4x4[3, 1] = internalArray[5];

      matrix4x4[0, 3] = internalArray[6];
      matrix4x4[1, 3] = internalArray[7];
      matrix4x4[3, 3] = internalArray[8];

      return matrix4x4;
    }

    /// Converts a flat affine display transform to a Matrix4x4.
    /// @remarks Represents a transformation matrix meant to be applied to column vectors
    /// ```
    /// | a  c  0  tx |
    /// | b  d  0  ty |
    /// | 0  0  1  0  |
    /// | 0  0  0  1  |
    /// ```
    internal static Matrix4x4 DisplayAffineToMatrix4x4(float[] affine)
    {
      var matrix4x4 = Matrix4x4.identity;
      // [row, col]
      matrix4x4[0, 0] = affine[0]; // a
      matrix4x4[1, 0] = affine[1]; // b
      matrix4x4[0, 1] = affine[2]; // c
      matrix4x4[1, 1] = affine[3]; // d
      matrix4x4[0, 3] = affine[4]; // tx
      matrix4x4[1, 3] = affine[5]; // ty

      return matrix4x4;
    }

    /// Converts a generic Matrix4x4 to a flat, column-major array.
    internal static float[] Matrix4x4ToInternalArray(Matrix4x4 matrix4x4)
    {
      float[] internalArray = new float[16];

      for (int col = 0; col < 4; col++)
      {
        for (int row = 0; row < 4; row++)
        {
          // internalArray is column-major
          internalArray[row + (col * 4)] = matrix4x4[row, col];
        }
      }

      return internalArray;
    }

    /// Scales the provided matrix by the scale.
    internal static void ApplyScale(ref Matrix4x4 matrix, float scale)
    {
      // Apply scale to translation
      // [row, col]
      matrix[0, 3] *= scale;
      matrix[1, 3] *= scale;
      matrix[2, 3] *= scale;
    }

    /// Applies the inverse of the scale to the provided matrix.
    internal static void ApplyInverseScale(ref Matrix4x4 matrix, float scale)
    {
      // invert the scale and scale!
      ApplyScale(ref matrix, 1 / scale);
    }
  }
}
