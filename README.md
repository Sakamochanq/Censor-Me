<div align="center">
    <a href="#">
        <img src="./assets/Censor-Me-Logo.png" width="500px">
    </a>
    <hr>
</div>

<br>

### How to use

**OpenCV**を使用し、人物画像から顔を検出し自動モザイク処理を行うツールです。`ファイル(F)`から画像を読み込み、実行ボタンをクリックすると処理が開始します。

<br>

> [!caution]
> OpenCVの精度の限界により、顔を誤検知する場合があります。

<br>
<br>

> [任意のバージョンでダウンロード](#)

<br>

<div align="center">
    <a href="#">
        <img src="./assets/Censor-Me-Demo.gif" width="400px">
    </a>
</div>

<br>
<hr>
<br>

### Build

<br>

```bash
git clone https://github.com/Sakamochanq/Censor-Me.git
cd Censor-Me/src
```

<br>

Buildを行う場合は、任意のバージョンで **OpenCVSharp** のパッケージをインストールしてください。

<br>

```bash
dotnet add package OpenCVSharp
```

<br>

Visual Studioから `*.sln` を開き Build を行ってください。

<br>
<hr>
<br>

### License

Release under the [MIT](./LICENSE) LICENSE

<br>

### Author

[Sakamochanq](https://github.com/Sakamochanq)
