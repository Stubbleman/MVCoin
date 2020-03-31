# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.5.0] - 2020-03-31
## Added
- Add different function for different mouse double click on Satellite
- Add Youtube viewer task
- Hide all stickies when double click again.

## [0.4.0] - 2020-03-29
### Added
- ```SCALE``` function in ```SatellitesControll``` class to make the whole orbit scale with sun.
- ```showDesktopSticky``` function.
- Add sticky icon

### Changed
- ```ChangeFormSize``` name in ```FormControll``` class to ```scaleFormSize```.
- Make each satellite can be moved individually.

### Fixed
- Satellites will not scale when sun change.


## [0.3.0] - 2020-03-28
### Added
- Add change log to repository.
- ```serailNum``` property of ```Satellite``` class.

### Changed
- Change ```Animation``` class timer. Fomrs.Timer -> Timers.Timer.