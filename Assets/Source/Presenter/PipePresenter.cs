using FireBalls3D.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireBalls3D.Presenter
{
    public class PipePresenter : Presenter
    {
        [SerializeField] private PresentersFactory _factory;
        [SerializeField] private AudioSource _audioClip;
        private int _index;

        public new Pipe Model => base.Model as Pipe;

        protected override void OnEnabling()
        {
            Model.SegmentsCreated += OnSegmentsCreated;
            Model.SegmentDestroyed += OnSegmentDestoryed;
        }

        protected override void OnDisabling()
        {
            Model.SegmentsCreated -= OnSegmentsCreated;
            Model.SegmentDestroyed -= OnSegmentDestoryed;
        }

        private void OnSegmentDestoryed()
        {
            _audioClip.Play();
        }

        private void OnSegmentsCreated(IEnumerable<Segment> segments)
        {
            Segment[] segmentsArray = segments.ToArray();

            for (int i = 0; i < Model.NumberSegments; i++)
            {
                _factory.CreateSegment(segmentsArray[i], _index);
                MoveIndex();
            }
        }

        private void MoveIndex()
        {
            _index++;

            if (_index >= _factory.NumberSegmentPresenters)
                _index = 0;
        }
    }
}