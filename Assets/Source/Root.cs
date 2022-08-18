using FireBalls3D.Input;
using FireBalls3D.Model;
using FireBalls3D.Presenter;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private CameraPresenter _cameraPresenter;
    [SerializeField] private TankPresenter _tankPresenter;
    [SerializeField] private PipePresenter _pipePresenter;
    [SerializeField] private ObstaclePatternPresenter _obstaclePatternPresenter;

    [Header("Spawn points")]
    [Space(20)]
    [SerializeField] private Point _pipePoint;
    [SerializeField] private Point _tankPoint;
    [SerializeField] private Point _obstaclePoint;
    [SerializeField] private Point _cameraPoint;

    private FireBalls3D.Model.Camera _camera;
    private PauseManager _pauseManager;
    private FireBalls3D.Model.Animator _tankAnimator;
    private FireBalls3D.Model.Animator _cameraAnimator;

    private Score _score;
    public Score Score => _score;

    private BoostedScoringPolicy _boostedScoringPolicy;

    private Shake _shake;
    private Recoil _recoil;

    private Tank _tank;
    public Tank Tank => _tank;

    private Health _health;
    public Health Health => _health;

    private Gun _gun;
    public ITimer GunTimer => _gun.Timer;

    private GunController _controller;
    private MissileDestroyer _missileDestroyer;

    private Pipe _pipe;
    public Pipe Pipe => _pipe;

    private ObstaclePattern _obstaclePattern;

    private void Awake()
    {

        _camera = new FireBalls3D.Model.Camera(_cameraPoint.Position, _cameraPoint.Rotation);
        _cameraPresenter.Init(_camera);
        _cameraAnimator = new FireBalls3D.Model.Animator(_camera);
        _pauseManager = new PauseManager();
            
        _obstaclePattern = new ObstaclePattern(_obstaclePoint.Position, Config.NumberObstacleLevels, Config.OffsetFromRotateCenter, Config.DistanceBetweenObstacles);
        _obstaclePatternPresenter.Init(_obstaclePattern);
        _obstaclePattern.CreateObstacles();

        _health = new Health(Config.TankHealth, _obstaclePattern.Obstacles);

        _tank = new Tank(_tankPoint.Position, Vector3.zero);
        Vector3 target = _pipePoint.Position;
        target.y = _tank.Position.y;
        _tank.LookAt(target);
        _tankPresenter.Init(_tank);
        _tankAnimator = new FireBalls3D.Model.Animator(_tank);

        _shake = new Shake(AnimationPriority.Normal);
        _recoil = new Recoil(AnimationPriority.Low);

        _controller = new GunController();
        _gun = new Gun(_tank, _tank.Forward, Config.GunReload);
        _controller.BindGun(_gun);

        _missileDestroyer = new MissileDestroyer(_gun, Config.MissileLifeTimeInSeconds);

        _pipe = new Pipe(_pipePoint.Position, Config.NumberSegments);
        _pipePresenter.Init(_pipe);
        _pipe.CreateSegments();

        _boostedScoringPolicy = new BoostedScoringPolicy(0.4f);
        _score = new Score(_pipe, _boostedScoringPolicy);

        _pauseManager.Register(_obstaclePattern);

        enabled = true;
    }

    private void Update()
    {
        _gun.Update(Time.deltaTime);
        _obstaclePattern.Update(Time.deltaTime);
        _missileDestroyer.Update(Time.deltaTime);
        _boostedScoringPolicy.Update(Time.deltaTime);
    }

    private void OnEnable()
    {
        _health.Damaged += OnHealthDamaged;
        _health.Died += OnTankDied;
        _gun.Shot += OnGunShot;
    }

    private void OnDisable()
    {
        _health.Damaged -= OnHealthDamaged;
        _health.Died -= OnTankDied;
        _gun.Shot -= OnGunShot;
    }

    private void OnHealthDamaged()
    {
        _cameraAnimator.StartAnimation(_shake);
    }

    private void OnTankDied()
    {
        DisableGunController();
        _pauseManager.Pause();
    }

    private void OnGunShot(Missile missile)
    {
        _tankAnimator.StartAnimation(_recoil);
        _factory.CreateMissile(missile);
    }

    public void DisableGunController()
    {
        _controller.Dispose();
    }
}