# Spritesheet *for Monogame*

Extremely basic APIs for creating sprite based animations.

## Install

Available on NuGet

[![NuGet](https://img.shields.io/nuget/v/Spritesheet.svg?label=NuGet)](https://www.nuget.org/packages/Spritesheet/)

## Quickstart

**Create**

```csharp
var sheet = new Spritesheet(texture2d).WithGrid((32, 32));
var anim = sheet.CreateAnimation((0, 1), (1, 1), (2, 1)).FlipX();
anim.Start(Repeat.Mode.Loop);
```
**Update**

```csharp
anim.Update(gameTime);
```
**Draw**

```csharp
spriteBatch.Draw(anim, new Vector2(64, 64));
```

## About

This project was inspired by [kikito/anim8](https://github.com/kikito/anim8) great library for LÖVE2D.

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)