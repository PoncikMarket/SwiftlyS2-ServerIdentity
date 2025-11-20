<p align="center">
  <img src="https://camo.githubusercontent.com/15411371090ab808f4644366f83bf2871aeacd208b2879fbe87f562a20150a3d/68747470733a2f2f70616e2e73616d7979632e6465762f732f56596d4d5845" />
</p>

A Counter-Strike 2 plugin that displays the server's public IP and port using a customizable command, prefix, and message template. Fully integrated with Swifty ChatColors and JSONC configuration.

## Features
- Display server IP and port using a customizable command (default: `!ip`).
- Fully customizable prefix, message format, and command name via JSONC.
- Supports Swifty `ChatColors` formatting inside the prefix/message.
- Optional console output for server administrators.
- Automatically generates configuration file (`cs2-serveridentity.jsonc`) if missing.
- Minimal, lightweight, fast — no external JSON libraries required.

## Requirements
- [SwiftlyS2](https://github.com/swiftly-solution/swiftlys2)

## Configuration
The plugin creates a JSONC configuration file (`ServerIdentityCFG.jsonc`) with the following options:

- **Command** — Command players use to view server IP (default: `"ip"`).
- **Prefix** — Chat prefix, supports `{green}`, `{white}`, `{yellow}` etc.
- **MessageFormat** — Output template with variables:
  - `{prefix}`
  - `{ip}`
  - `{port}`
- **ConsolePrint** — Whether to print IP/port info in the server console.

### Example Config:
```bash
jsonc{
  "ServerIdentity": {
    "Command": "ip",
    "Prefix": "{green}[PoncikMarket]{white}",
    "MessageFormat": "{prefix} Server Address: {yellow}{ip}:{port}",
    "ConsolePrint": true
  }
}
```
## Commands

- `!ip` or `/ip`: Shows your server’s IP and port.

## Author
- PoncikMarket (Discord: `poncikmarket`)
