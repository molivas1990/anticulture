## Procedure ##
Let's say we have a [genome](Genome.md) represented by a set of 10 numbers: {8,36,42,75}.
By normalizing the genome, we would reduce each numbers so they don't pass a predefined limit. Let's say the limit is 50, each numbers would be divided by the biggest number: 75 and multiplied by the limit: 50.

## Why do we need this? ##
Because Genes's float number could grow so large that the limit of float numbers could be near.