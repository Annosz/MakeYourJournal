import Todo from "../models/todo.model";
import Note from "../models/note.model";

class Article {
    id: number;
    title: string;
    topic: string;
    issueId: number;
    todoCount: number;
    notesCount: number;
    todos: Todo[];
    notes: Note[];
}

export default Article;