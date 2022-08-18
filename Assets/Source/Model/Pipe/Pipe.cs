using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Pipe : Transformable
    {
        private List<Segment> _segments;
        private Vector3 _position;
        private int _numberSegments;

        public event Action<IEnumerable<Segment>> SegmentsCreated;
        public event Action SegmentDestroyed;

        public Pipe(Vector3 position, int numberSegments)
        {
            _segments = new List<Segment>();
            _position = position;
            _numberSegments = numberSegments;
        }

        public int NumberSegments => _segments.Count;

        public void CreateSegments()
        {
            Vector3 segmentSize = new Vector3(0.8f, 0.7f, 0.8f);

            Segment previousSegment = CreateSegment(_position, Vector3.zero, segmentSize);

            for (int i = 1; i <= _numberSegments - 1; i++)
            {
                Segment segment = CreateSegment(previousSegment, Vector3.zero, segmentSize);
                previousSegment = segment;
            }

            OnEnable();

            SegmentsCreated?.Invoke(_segments);
        }

        private void OnEnable()
        {
            _segments.ForEach(segment => segment.Destroying += OnSegmentDestroy);
        }

        private void OnDisable()
        {
            _segments.ForEach(segment => segment.Destroying -= OnSegmentDestroy);
        }

        private void OnSegmentDestroy(Segment segment)
        {
            segment.Destroying -= OnSegmentDestroy;
            _segments.Remove(segment);

            Vector3 offset = Vector3.down * segment.Scale.y;

            _segments.ForEach(s => s.MoveTo(offset));

            SegmentDestroyed?.Invoke();

            if(_segments.Count == 0)
            {
                Dispose();
            }
        }

        private Segment CreateSegment(Vector3 position, Vector3 rotation, Vector3 size)
        {
            Segment segment = new Segment(position + Vector3.up * size.y * 0.5f, rotation, size);
            _segments.Add(segment);

            return segment;
        }

        private Segment CreateSegment(Segment previous, Vector3 rotation, Vector3 size)
        {
            return CreateSegment(previous.Position + Vector3.up * previous.Scale.y * 0.5f, rotation, size);
        }
    }
}
