# Changelog

All notable changes to Pure.RelationalSchema.Storage.HashCodes are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.2.2.0] — 2026-05-20

- Maintenance release: dependency and build updates.

## [0.1.0-preview.2.1.0] — 2025-12-09

### Added
- Multi-targeting: the package now also builds for `net7.0`, `net8.0`, and
  `net10.0`, in addition to `net9.0`.

## [0.1.0-preview.2.0.0] — 2025-11-04

- Maintenance release: dependency and build updates.

## [0.1.0-preview.1.0.0] — 2025-10-16

### Changed
- **Breaking:** `StoredSchemaDataSetHash` now folds a hash of the schema
  itself into the computed value, in addition to the per-table hashes —
  hashes produced by this version differ from `0.1.0-preview.0.1.0` for the
  same data.

## [0.1.0-preview.0.1.0] — 2025-09-29

### Added
- **`CellHash`** — deterministic `IDeterminedHash` for an `ICell` value.
- **`RowHash`** — deterministic `IDeterminedHash` for an `IRow`, combining
  each cell's column and value hashes.
- **`StoredTableDataSetHash`** — deterministic `IDeterminedHash` for an
  `IStoredTableDataSet`, combining the table schema hash and all row hashes.
- **`StoredSchemaDataSetHash`** — deterministic `IDeterminedHash` for an
  `IStoredSchemaDataSet`, combining the table hash and row-set hash of every
  table in the dataset.
