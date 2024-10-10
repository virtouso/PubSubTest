using PubSubTest.Types;


Publisher publisher = new Publisher();
Subscriber sub1 = new Subscriber("Subscriber 1", publisher);
Subscriber sub2 = new Subscriber("Subscriber 2", publisher);

// Publish messages
publisher.Publish("Hello Receivers!");
publisher.Publish("Testing Second Message.");