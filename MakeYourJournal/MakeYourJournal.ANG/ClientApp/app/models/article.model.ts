import Todo from "../models/todo.model"

class Article {
    id: number;
    title: string;
    topic: string;
    issueId: number;
    todoCount: number;
    notesCount: number;
    todos: Todo[];
}

export default Article;