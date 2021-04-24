public interface EventSystemVisitor {
    
    void Visit(object? sender, AckableEventArgs v);
    void Visit(object? sender, CharSequenceEventArgs v);

    void Visit(object? sender, AdvanceCharSequenceEventArgs v);

    void Visit(object? sender, CancelCharSequenceEventArgs v);


}