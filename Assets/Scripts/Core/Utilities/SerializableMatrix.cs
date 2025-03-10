using System;
using UnityEngine;
using UnityEngine.WSA;

namespace Moonthsoft.Core
{
    /// <summary>
    /// The serializable matrix is ​​used to be able to display matrices in the Unity inspector, since by default it's not possible to do so.
    /// See the SerializableMatrixDrawer class in the Editor folder to see how it is drawn in the inspector.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the matrix, for example, int, float, string...</typeparam>
    [System.Serializable]
    public class SerializableMatrix<T>
    {
        [SerializeField] private int _sizeRow = 0;
        [SerializeField] private int _sizeColumn = 0;
        [SerializeField] private SerializableArray[] _serializedMatrix;

        public SerializableArray[] Matrix { get { return _serializedMatrix; } }


        public SerializableMatrix(T[][] matrix)
        {
            _sizeRow = matrix.Length;

            if (_sizeRow > 0)
            {
                _sizeColumn = matrix[0].Length;
            }

            _serializedMatrix = new SerializableArray[_sizeRow];

            for (int i = 0; i < _serializedMatrix.Length; ++i)
            {
                _serializedMatrix[i] = new SerializableArray(_sizeColumn);
            }

            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    _serializedMatrix[i].array[j] = matrix[i][j];
                }
            }
        }

        public void ValidateMatrix()
        {
            if (_sizeRow != _serializedMatrix.Length || (_serializedMatrix.Length > 0 && _sizeColumn != _serializedMatrix[0].array.Length))
            {
                var oldMatrix = _serializedMatrix;

                _serializedMatrix = new SerializableArray[_sizeRow];

                for (int i = 0; i < _serializedMatrix.Length; ++i)
                {
                    _serializedMatrix[i] = new SerializableArray(_sizeColumn);
                }

                int sizeMatrix = Math.Min(oldMatrix.Length, _serializedMatrix.Length);

                for (int i = 0; i < sizeMatrix; ++i)
                {
                    int sizeArray = Math.Min(oldMatrix[i].array.Length, _serializedMatrix[i].array.Length);

                    for (int j = 0; j < sizeArray; ++j)
                    {
                        _serializedMatrix[i].array[j] = oldMatrix[i].array[j];
                    }
                }
            }
        }


        [System.Serializable]
        public class SerializableArray
        {
            [SerializeField] public T[] array;

            public SerializableArray(int sizeArray)
            {
                array = new T[sizeArray];
            }
        }
    }
}