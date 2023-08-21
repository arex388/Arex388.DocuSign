using Arex388.DocuSign.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<DocuSignClientFactory>();
BenchmarkRunner.Run<Envelope>();