# Restaurants and courses
This tutorial will show you how to modelize a fictive restaurants and courses problem. In this tutorial, we'll work with a model where restaurants from a chain are suggesting a few main courses. The list of courses at the menu of a given restaurant will evolve over time. 

[001]

Naturally, the name of the restaurant and the name of the course are also subject to change and we want to keep an history of these changes. Each course has also information about how spicy is it. The chain has introduced four levels to inform customers, how spicy are the courses. 

[002]

The chain of restaurant wants to analyze which course are suggested in which restaurant at a given date or during a period but also to know how many of these courses have been served. They also want to analyze if the different names (restaurant and courses) have an influence on these quantity.

## Anchors
Based on the desciption above, we can easily identify two entities: *restaurant* and *course*. For each of them, we need to create an anchor. Each anchor contains a business key (*VAT number*/*Course code*) and a surrogate key.

[003]

## Infos
Info tables contain the attributes of the entities. For the restaurant, it'll be the *name* and *address* and for the course we'll start with the label and take into account how spicy is it in the next paragraph. Each info stored in an Info table will also be assigned a surrogate key.

[004]

## Knots
The spiciness of a course admits a limited set of values: mild, medium, hot. We can create a knot with these three values. The table will contain a surrogate key and a label. It's often suitable to add a *sort* attribute to fix the order in reports or dropdownlist. The link between the knot and the info is directly in the table info

[005]

## Links between anchors and infos
To create the relation between an anchor table and its corresponding info table, we must create a link table. This table will contains the respective surrogate keys of the two entities but also a surrogate key of the discretisation of Time dimension.

[006]

## Links between two anchors
We must do the same for the relations between the two anchors. Once again we'll create a many-2-many table with the surrogate keys of the two anchors and the surrogate of the *Time*.

[007]

## Fact
The fact is modeled the same way than in any Kimball model. A fact table with the surrogate keys of the corresponding dimensions (including time) and a measure column for the *count*.

[008]