# Changelog

All notable changes to shitass are documented here.  
Build numbers follow the format YYMMDD.

---
## [251005] - 2025-10-05
### Added
- base64 command
- calc command
- hash command
- port command
- process command
- settings command
- Much more customization
- Auto update downloading

### Changed
- More settings and less jank

---

## [251004] - 2025-10-04
### Added
- Dynamic command loading
- Version checking against github pages

### Changed
- Commands now live in separate files for better organization
- Restart command fixed
- Sysinfo command has more info
- Removed echo newlines
- Title no longer requires a restart

### Removed
- Ascii text art generator because it sucked ass

---

## [250305] - 2025-03-05
### Added
- Donut spinning animation command

### Fixed
- Password generator bug
- Various formatting issues

---

## [250223] - 2025-02-23
### Added
- Password generator command

### Changed
- Settings file handling improvements

### Fixed
- Various bug fixes

---

## [250218] - 2025-02-18
### Added
- ASCII art generator using external API
- Rock paper scissors
- Directory deletion support to `del` command

### Changed
- Optimized `ping` and `shorten` commands
- Improved command alias handling

### Fixed
- Color command crashes
- Drive info console clearing bug

---

## [250217] - 2025-02-17
### Added
- Echo command
- Color command for customizing console appearance
- Window title reset functionality (`title -reset`)
- Gigabyte display (from bytes) for drive info

### Changed
- Build naming convention switched to date format (YYMMDD)
- Enhanced sysinfo command with additional details
- Title command now supports spaces and persists settings
- Clear command preserves console branding
- Improved error handling across multiple commands
- Better fallback behavior in help system

---

## [220309] - 2022-03-09
### Fixed
- High CPU usage in window title refresh

---

## [220308] - 2022-03-08
### Removed
- Time command (redundant functionality)

### Changed
- Optimized title refresh mechanism to reduce CPU usage
- Reformatted changelog structure

### Fixed
- Title display bug

---

## [220226] - 2022-02-26
### Added
- IPv6 support for ping command
- Real-time date/time in window title

---

## [220110] - 2022-01-10
### Added
- v.gd as alternative URL shortener option

---

## [210819] - 2021-08-19
### Fixed
- Sysinfo incorrectly reporting NT 6.0 on Windows 8.1+ (thanks [@plexthedev](https://github.com/plexthedev))

---

## [210720b] - 2021-07-20
### Added
- URL shortener (is.gd)
- Time command
- Coin flip command

---

## [210720a] - 2021-07-20
### Added
- Ping command with network diagnostics

---

## [210718] - 2021-07-18
### Added
- File deletion command (`del`)

### Changed
- Improved help command formatting

---

## [210708] - 2021-07-08
### Added
- Initial repository creation
- System information command
- Basic command structure
