# Shipment Search Flow

```mermaid
flowchart TD
  Q[Type query] --> D{Debounce 200ms}
  D -->|ok| F[Filter by id/customer/status (case-insensitive)]
  F --> R{Matches?}
  R -->|no| N[Show "No results"]
  C[Clear] --> X[Reset]
