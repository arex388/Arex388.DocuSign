# BENCHMARKS



### .NET 7

#### DocuSignClientFactory

| Method               |     Mean |   Error |  StdDev |   Gen0 | Allocated |
| -------------------- | -------: | ------: | ------: | -----: | --------: |
| CreateAndCacheClient | 306.1 ns | 0.98 ns | 0.91 ns | 0.0420 |     176 B |



#### Create and Void Envelope

| Method      |    Mean |    Error |   StdDev | Allocated |
| ----------- | ------: | -------: | -------: | --------: |
| CreateAsync | 1.279 s | 0.2360 s | 0.2318 s |  47.08 KB |