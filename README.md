# LeoEcsLite - Snail Animation
Удобное взаимодействие с Unity Animator через ECS

## Установка

> **ВАЖНО!** Зависит от [LeoEcsLite](https://github.com/Leopotam/ecslite)

> **ВАЖНО!** *Временно* Зависит от [LeoEcsLiteEntityConverter](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter)

### В виде Unity модуля

Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"com.qw1nt.snailanimation": "https://github.com/Qw1nt/LeoEcsLite.SnailAnimation.git",
```

## Интеграция

### Подключение системы

```csharp
private IEcsSystems _systems;

private void Start()
{
    _systems = new EcsSystems(new EcsWorld());
    _systems
        .Add(new TestSystem1())
        .AddAnimationSystem() // Добавляем все необходимые системы
        .Init();
}

private void Update() 
{
    _systems?.Run();
}
``` 
