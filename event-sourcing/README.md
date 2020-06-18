# Event Sourcing

This folder contains a sample project for [Event Sourcing](https://de.wikipedia.org/wiki/Event_Sourcing) (for simplicity without using events though).

The typical example of a bank account is used, as it perfectly demonstrates that a sequence of balance modifications ([IncomingPayment](./Transactions/IncomingPayment.cs)s and [OutgoingPayments](./Transactions/OutgoingPayment.cs)) can be used to determine the current balance at any point in time.

The key aspect of Event Sourcing is that the current balance isn't stored anywhere in the code but rather evaluated each time it is requested. That's why it often includes the [CQS](https://de.wikipedia.org/wiki/Command-Query-Separation) principle to separate the balance modification (commands) from reading access to it (queries).
