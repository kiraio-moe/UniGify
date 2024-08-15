# UniGify

A GIF decoder/viewer for Unity.

## Installation

- UPM

  You can install the plugin using Package Manager > Add package from Git url:

  ```
  https://github.com/kiraio-moe/UniGify.git
  ```

## How To Use

> [!IMPORTANT]  
> Because of the nature of Unity Editor to treat `.gif` file as a `Texture`,
> you need to append your `.gif` file with `.bytes`, so Unity will import the asset as is.
>
> Example: `mygif.gif` => `mygif.gif.bytes`

- Add your desired GIF viewer components (`GifSpriteRenderer`, `GifImage`, `GifRawImage`) to the GameObject.
- Assign the `Source` field of the viewer.
- You can toggle `Play On Start` to autoplay the GIF at start or not.
