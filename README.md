# Tibre
Tibre is a modeling methodology and a framework to support special data warehouses centered around time-based relations between entities

[![Build status](https://ci.appveyor.com/api/projects/status/ntr1hq0k2gvti5sj?svg=true)](https://ci.appveyor.com/project/Seddryck/tibre)
![Still maintained](https://img.shields.io/maintenance/yes/2017.svg)
![nuget](https://img.shields.io/nuget/v/Tibre.svg) 
![GitHub release](https://img.shields.io/github/release/Seddryck/Tibre.svg?maxAge=2592000)

## Anchor
Anchors contain a list of unique business keys with low propensity to change. They also contain a surrogate key for each Anchor item and optionally metadata describing the origin of the business key. 

The Anchor contains at least the following fields:

* a surrogate key, used to connect the other structures to this table.
* a business key, the driver for this hub. The business key can consist of multiple fields.
* optionally, you can also have metadata fields with information about manual updates (user/time) and the extraction date.

## Info
Infos hold descriptive attributes. Where the anchors and links provide the structure of the model, the infos provide the "meat" of the model, the context for the business processes that are captured in anchors and links. Usually the attributes are grouped in Infos by source system. However, descriptive attributes such as size, cost, speed, amount or color can change at different rates, so you can also split these attributes up in different satellites based on their rate of change.

## Link
Associations between two (or more) anchors or between an anchor and a related Info are modeled using link tables. These tables are basically many-to-many join tables. The Time is modeled as a first class-citizen in these tables To achieve this, Time is discretized to the granularity of the minute/hour/day/month/year according to the needs of the model.

Links contain the surrogate keys for the anchors and infos that are linked but also the surrogate key for the Time dimension according to the granularity of the model.

## Knot
Knots are reference tables containing a fairly small set of distinct values. They could be considered as dictionary lookup to qualify facts, attributes stored in info or links.

## Fact
Facts are the classical representation of a fact in data warehouse. These tables have foreign keys pointing to Anchors, also contain a relation to the Time dimension but also have values directly stored in these tables.
