# Event Sourcing

This folder contains a sample project for [EventSourcing](https://de.wikipedia.org/wiki/Event_Sourcing) (for simplicity without using events though).

The typical example of a bank account is used, as it perfectly demonstrates that a sequence of balance modifications ([IncomingPayment](./Transactions/IncomingPayment.cs)s and [OutgoingPayments](./Transactions/OutgoingPayment.cs)) can be used to determine the current balance at any point in time.

The key aspect of the EventSourcing pattern is that the current balance isn't stored anywhere in the code but rather evaluated each time it is requested.