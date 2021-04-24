public interface EventSystemVisitable {
    void Accept(object? sender, EventSystemVisitor v);
}