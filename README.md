# Remove Unredacted Images

When I take photos of protests, I routinely blur out the faces of any non-public figures. I generally do this manually, using [Paint.Net](https://www.getpaint.net/) and then save the image with the same name, plus `(redacted)`.

This C# console app will look at all files in the current directory that end `(redacted).jpg` and delete any file with the same name but without the `(redacted)` label, listing each file as it does so.

It can optionally accept a flag of `--dry-run`. If this flag is not provided, the user will be prompted:

```plaintext
Beginning deletion without dry run. Do you wish to proceed?
[Y]es, [D]ry run, [N]o (exit).
```

If the user presses `Y`, the script will run and delete files as appropriate. Pressing `D` will switch to dry run, where the script will run and list files without deleting them. Pressing `N` or `X` will cause the utility to exit.
