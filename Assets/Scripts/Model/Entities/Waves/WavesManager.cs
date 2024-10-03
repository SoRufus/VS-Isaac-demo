using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Entities.Spawner;
using Model.Settings;
using Zenject;

namespace Model.Entities.Waves
{
    public class WavesManager
    {
        public WaveConfig CurrentWaveConfig { get; private set; }
        
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        
        private CancellationTokenSource _cancellationTokenSource;
        private int _waveIndex;
        
        [Inject]
        public WavesManager(GameSettings settings)
        {
            _enemySpawnerConfig = settings.EnemySpawnerConfig;
            CurrentWaveConfig = settings.EnemySpawnerConfig.GetWave(_waveIndex);
        }
        
        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            ProcessWave(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask ProcessWave(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_enemySpawnerConfig.WavesChangingFrequency, cancellationToken: cancellationToken);
                NextWave();
            }
        }

        private void NextWave()
        {
            _waveIndex++;
            CurrentWaveConfig = _enemySpawnerConfig.GetWave(_waveIndex);
        }
    }
}