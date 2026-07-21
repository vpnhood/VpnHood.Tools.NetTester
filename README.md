# VpnHood NetTester

A network performance testing tool that supports multiple protocols including TCP, HTTP, HTTPS, and QUIC. NetTester can run as both a server and client to measure upload and download speeds across different network protocols.

## Features

- **Multiple Protocol Support**: Test network performance using TCP, HTTP, HTTPS, and QUIC protocols
- **Server/Client Architecture**: Run as a standalone server, client, or both simultaneously
- **Performance Measurement**: Measure upload and download speeds with configurable data sizes
- **Concurrent Testing**: Support for both single and multi-connection tests
- **Flexible Configuration**: Command-line arguments for easy customization
- **Domain Support**: Test with custom domains and SSL/TLS certificates

## Requirements

- .NET 10.0 or later

## Installation

Install as a .NET tool — no build required:

```bash
# Global install
dotnet tool install --global VpnHood.Tools.NetTester

# ...or pin it per repository (recommended for teams and CI)
dotnet new tool-manifest
dotnet tool install VpnHood.Tools.NetTester
```

The command is `nettester` (or `dotnet nettester` for a local install).

To build from source instead:

```bash
dotnet build VpnHood.Tools.NetTester.slnx
```

## Usage

### Command Syntax

```bash
nettester /ep <endpoint> [options]
```

### Options

| Option | Description | Default |
|--------|-------------|---------|
| `/ep <ip:port>` | Server endpoint (IP address and port) | Required |
| `/server` | Run as server | - |
| `/client` | Run as client | - |
| `/tcp <port>` | TCP port for testing | 0 (disabled) |
| `/http <port>` | HTTP port for testing | 0 (disabled) |
| `/https <port>` | HTTPS port for testing | 0 (disabled) |
| `/quic <port>` | QUIC port for testing | 0 (disabled) |
| `/up <size>` | Upload size in MB | 100 |
| `/down <size>` | Download size in MB | 100 |
| `/domain <domain>` | Domain name for SSL/TLS | Random domain |
| `/valid-domain` | Use a valid domain | false |
| `/multi <count>` | Number of concurrent connections | 0 (disabled) |
| `/single <bool>` | Enable single connection test | true |
| `/timeout <seconds>` | Timeout in seconds | 15 |
| `/url <url>` | Test a specific URL | - |
| `/url-ip <ip>` | IP address for URL testing | - |
| `/out <file>` | Output report file | - |
| `/debug` | Enable debug mode | false |

### Examples

#### Start a Server

Run a server listening on endpoint `192.168.1.100:44`:

```bash
nettester /ep 192.168.1.100:44 /server
```

#### Run Client Tests

Test TCP, HTTP, and HTTPS with custom upload/download sizes:

```bash
nettester /ep 192.168.1.100:44 /client /tcp 33700 /http 8080 /https 443 /up 60 /down 60
```

#### Test with Multiple Connections

Run tests with 4 concurrent connections:

```bash
nettester /ep 192.168.1.100:44 /client /tcp 33700 /multi 4
```

#### Combined Server and Client

Run both server and client on the same machine:

```bash
nettester /ep 1.2.3.4:44 /server /client /tcp 33700 /http 8080 /https 443 /quic 443 /up 60 /down 60 /domain www.foo.com /multi 4
```

#### Test External URL

Test download speed from an external URL:

```bash
nettester /ep 192.168.1.100:44 /client /url https://example.com/file.bin
```

#### Stop a Running Server

```bash
nettester stop
```

This creates a stop command file that the server monitors and uses to gracefully shut down.

## Protocol Details

### TCP Testing
Direct TCP socket connections for raw throughput testing.

### HTTP Testing
Standard HTTP/1.1 connections for web-based performance testing.

### HTTPS Testing
Secure HTTP connections with SSL/TLS encryption. Supports custom domains and certificates.

### QUIC Testing
Modern QUIC protocol testing with built-in encryption and improved performance over traditional TCP.

## Output

The tool logs performance metrics including:
- Connection establishment time
- Upload speed
- Download speed
- Total data transferred
- Protocol-specific metrics

Use the `/out` option to save results to a file for later analysis.

## Architecture

The project is organized into the following components:

- **Servers**: Server-side implementations for each protocol
- **Clients**: Client-side implementations and test orchestration
- **Testers**: Protocol-specific tester implementations (TCP, HTTP, HTTPS, QUIC)
- **Utils**: Utility classes for logging, argument parsing, and performance measurement

## License

LGPL-2.1-only

## Copyright

© OmegaHood LLC. All rights reserved.

## Project Links

- [VpnHood Project](https://github.com/vpnhood/vpnhood)
- [NetTester Repository](https://github.com/vpnhood/VpnHood.Tools.NetTester)
- [NuGet Package](https://www.nuget.org/packages/VpnHood.Tools.NetTester)

## Contributing

This project is part of the VpnHood ecosystem. For contributions and issues, please visit the project repository.
