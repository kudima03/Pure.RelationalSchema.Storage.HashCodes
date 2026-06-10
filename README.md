# Pure.RelationalSchema.Storage.HashCodes

Deterministic hash code implementations for relational schema storage types in the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.Storage.HashCodes/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Storage.HashCodes/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.Storage.HashCodes/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Storage.HashCodes/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.Storage.HashCodes)](https://www.nuget.org/packages/Pure.RelationalSchema.Storage.HashCodes)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.Storage.HashCodes` provides deterministic, SHA-256-based hash records for the core storage data types: cells, rows, table datasets, and schema datasets. These hashes are used as dictionary keys and for equality checks when reconstructing stored schema data sets during deserialization or projection.

## Hash Types

| Type | Hashed fields |
|------|---------------|
| `CellHash` | Cell value (`IString`) |
| `RowHash` | All column-cell pairs in the row |
| `StoredTableDataSetHash` | Table schema + all rows |
| `StoredSchemaDataSetHash` | Schema + all table datasets |

All types are `sealed record` implementing `IDeterminedHash` and `IEnumerable<byte>`. Each type embeds a unique 16-byte domain prefix before hashing to prevent cross-type collisions.

## Dependencies

- [`Pure.RelationalSchema.Storage.Abstractions`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Abstractions) — `ICell`, `IRow`, `IStoredTableDataSet`, `IStoredSchemaDataSet` interfaces
- [`Pure.HashCodes`](https://github.com/kudima03/Pure.HashCodes) — deterministic hash computation
