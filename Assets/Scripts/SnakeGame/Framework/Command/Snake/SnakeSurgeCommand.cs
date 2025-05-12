using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SnakeSurgeCommand : ICommand
{
    private Snake snake;
    private List<Transform> list;

    public SnakeSurgeCommand(Snake snake)
    {
        this.snake = snake;
        list = snake.list;
    }

    public void Execute()
    {
        if (snake == null) return;

        SurgeAsync().Forget(); // 使用 Forget() 避免无等待的异步方法抛出异常
    }

    private async UniTask SurgeAsync()
    {
        if (snake == null || list == null || list.Count == 0 || snake.Obj == null) return;

        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (item == null) continue;

            // 确保 item 仍然存在
            var tokenSource = item.GetCancellationTokenOnDestroy();

            if (tokenSource.IsCancellationRequested || item == null) continue;

            Vector3 originalScale = snake.data.snakeScale * Vector3.one;
            Vector3 targetScale = originalScale * 2f;
            float duration = 0.1f;

            await ScaleTransform(item, originalScale, targetScale, duration, tokenSource);
            await ScaleTransform(item, targetScale, originalScale, duration, tokenSource);
        }
    }

    private async UniTask ScaleTransform(Transform item, Vector3 from, Vector3 to, float duration, CancellationToken token)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (token.IsCancellationRequested || item == null) return;

            item.localScale = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            await UniTask.Yield(token).SuppressCancellationThrow();
        }

        if (item != null) item.localScale = to; // 最终确保 item 设为目标值
    }
}
