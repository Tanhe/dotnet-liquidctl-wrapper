# dotnet-liquidctl-wrapper
Minimal example that shows that the output of the tool cannot be parsed when invoked on Windows without a Terminal.

Assumes `liquidctl` is on the path.

Assume `dotnet` is installed (3.1+)

Run `dotnet run`....

Current output:

```
StdOut: NZXT Kraken X (X42, X52, X62 or X72)
StdErr: ERROR: Unexpected error with NZXT Kraken X (X42, X52, X62 or X72)Traceback (most recent call last):UnicodeEncodeError: 'charmap' codec can't encode characters in position 0-2: character maps to <undefined>
0
```