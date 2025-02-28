namespace RubixCube.Core {

    public class MoveStack {
        private Move[] stack;
        public Move[] Moves => stack[..Count];
        public int Count;
        private int DefaultCapacity = 100;
        
        public MoveStack() {
            stack = new Move[DefaultCapacity];
            Count = 0;
        }
        
        public MoveStack(int capacity) {
            stack = new Move[capacity];
            Count = 0;
        }
        
        public void Push(Move move) {
            stack[Count++] = move;
        }
        
        public Move PeekLast() {
            if (Count == 0)
                return Move.NullMove;
            return stack[Count - 1];
        }
        
        public Move PeekSecondLast() {
            if (Count <= 1)
                return Move.NullMove;
            return stack[Count - 2];
        }
        
        public Move Pop() {
            if (Count == 0)
                return Move.NullMove;
            return stack[--Count];
        }
    }
}