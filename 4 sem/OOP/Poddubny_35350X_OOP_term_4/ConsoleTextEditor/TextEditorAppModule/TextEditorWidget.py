from textual.app import App, ComposeResult
from textual.widgets import TextArea, Static, Input
from textual.containers import Horizontal, VerticalScroll
from rich.markdown import Markdown
from .TextEditor import TextEditor
from textual import on
from rich.console import Console
from textual import events
from rich.console import Console
from rich.text import Text
from rich.panel import Panel
from io import StringIO
import re


class TextEditorWidget(App):
    
    def __init__(self, text: str, editor: TextEditor):
        super().__init__()
        self.text = text
        self.editor = editor
        self.ta = TextArea(text, language="markdown", id="editor")
        self.search_input = Input(placeholder="find... (enter find, esc close f3 next shift+f3 previous)", 
                                id="search-input", 
                                classes="hidden")
        self.current_search_matches = []
        self.current_search_index = -1
        
        if(not self.get_redact_or_visitor()):
            self.ta.disabled = True

        self.preview = Static("", id="preview")
        if(self.editor.current_document._name[-3:] == ".md"):
            try:
                self.preview.update(Markdown(self.ta.text))
            except Exception:
                self.preview.update("wrong input")

        elif(self.editor.current_document._name[-5:] == ".json" or \
        self.editor.current_document._name[-4:] == ".txt" or \
        self.editor.current_document._name[-4:] == ".xml"):
            try:
                self.preview.update(self.ta.text)
            except Exception:
                self.preview.update("wrong input")

        else: self.preview.update("not supported")
        self.hint_text = "ctrl+q quit, ctrl+s save localy, ctrl+o save in cloud, ctrl+n show updates of document, ctrl+f find, ctrl+t themes\n| notifications-> "
        self.updates = Static(f"{self.hint_text}", id="bottom-panel")                 

    CSS = """
    Horizontal {
        height: 100%;
        width: 100%;
    }
    
    #editor {
        width: 50%;
        height: 100%;
        border: solid $accent;
    }
    
    #preview-container {
        width: 50%;
        height: 90%;
        overflow-y: auto;
    }
    
    #preview {
        width: 100%;
        padding: 1;
    }
    
    #bottom-panel {
        dock: bottom;
        width: 100%;
        height: auto;
        padding: 1;
        background: $panel;
        color: $text;
    }
    
    #search-input {
        dock: top;
        width: 100%;
        margin: 0 0 1 0;
        background: $boost;
        color: $text;
        border: none;
    }
    
    .hidden {
        display: none;
    }
    """

    BINDINGS = [
        ("ctrl+q", "quit", "Quit"),
        ("ctrl+s", "save", "Save"),
        ("ctrl+o", "save_cloud", "Save"),
        ("ctrl+n", "show_notifications", ""),
        ("ctrl+f", "search", "Find"),
        ("f3", "find_next", "Find Next"),
        ("shift+f3", "find_previous", "Find Previous"),
        ("escape", "hide_search", "Hide Search"),
        ("ctrl+t", "change_theme", "Change theme")
    ]

    def compose(self) -> ComposeResult:
        yield self.search_input
        with Horizontal():
            yield self.ta
            with VerticalScroll(id="preview-container"):
                yield self.preview
        yield self.updates

    def action_change_theme(self)-> None:
        if self.theme == "textual-dark":
            self.theme = "solarized-light"
        elif self.theme == "solarized-light":
            self.theme = "textual-dark"

    def on_mount(self) -> None:
        self.ta.focus()
        self.theme = "textual-dark"

    def action_search(self) -> None:
        self.search_input.remove_class("hidden")
        self.search_input.focus()
        if self.ta.selected_text:
            self.search_input.value = self.ta.selected_text

    def action_hide_search(self) -> None:
        self.search_input.add_class("hidden")
        self.ta.focus()
        self.clear_search_highlights()

    def clear_search_highlights(self) -> None:
        self.ta.highlight_ranges = []
        self.current_search_matches = []
        self.current_search_index = -1

    @on(Input.Submitted, "#search-input")
    def perform_search(self, event: Input.Submitted) -> None:
        search_text = event.value
        if not search_text:
            self.clear_search_highlights()
            return

        text = self.ta.text
        self.current_search_matches = list(re.finditer(re.escape(search_text), text, re.IGNORECASE))
        
        if not self.current_search_matches:
            self.updates.update(f"{self.hint_text}No matches found")
            return

        highlight_ranges = []
        for match in self.current_search_matches:
            start = match.start()
            end = match.end()
            highlight_ranges.append((start, end))
        
        self.ta.highlight_ranges = highlight_ranges
        self.current_search_index = 0
        self._move_to_match(self.current_search_index)
        self.updates.update(f"{self.hint_text}Found {len(self.current_search_matches)} matches")

    def _move_to_match(self, index: int) -> None:
        if not self.current_search_matches or index < 0 or index >= len(self.current_search_matches):
            return

        match = self.current_search_matches[index]
        # Устанавливаем курсор и выделение
        self.ta.cursor_location = (self._get_line_col(match.start()))
        # Прокручиваем к курсору

    def _get_line_col(self, pos: int) -> tuple[int, int]:
        text = self.ta.text
        lines = text[:pos].split('\n')
        line = len(lines) - 1
        col = len(lines[-1]) if line > 0 else pos
        return (line, col)

    def action_find_next(self) -> None:
        if not self.current_search_matches:
            return
            
        self.current_search_index = (self.current_search_index + 1) % len(self.current_search_matches)
        self._move_to_match(self.current_search_index)

    def action_find_previous(self) -> None:
        if not self.current_search_matches:
            return
            
        self.current_search_index = (self.current_search_index - 1) % len(self.current_search_matches)
        self._move_to_match(self.current_search_index)

    def on_key(self, event: events.Key) -> None:
        if event.key == "ctrl+f":
            event.prevent_default()
            self.action_search()
            return
            
        key = event.key
        if key == "down":
            scroll_container = self.query_one("#preview-container", VerticalScroll)
            scroll_container.scroll_down()
        if key == "up":
            scroll_container = self.query_one("#preview-container", VerticalScroll)
            scroll_container.scroll_up()

    def get_redact_or_visitor(self):
        return self.editor.redactor_or_visitor(self.editor.current_document._name)

    def on_text_area_changed(self, event: TextArea.Changed) -> None:
        if(self.editor.current_document._name[-3:] == ".md"):
            try:
                self.preview.update(Markdown(event.text_area.text))
            except Exception:
                self.preview.update("wrong input")

        elif(self.editor.current_document._name[-5:] == ".json" or \
        self.editor.current_document._name[-4:] == ".txt" or \
        self.editor.current_document._name[-4:] == ".xml"):
            try:
                self.preview.update(event.text_area.text)
            except Exception:
                self.preview.update("wrong input")

        else: self.preview.update("not supported")
        
    def action_show_notifications(self) -> None:
        self.preview.update("\n".join(self.editor.authorized_user.notifications))
        self.editor.authorized_user.notifications.clear()
    
    def action_quit(self) -> None:
        self.exit()

    def action_save(self) -> None:
        if (self.get_redact_or_visitor):
            self.editor.text = self.ta.text
            self.editor.save_document()
            self.updates.update(f"{self.hint_text}saved locally")
        else: self.updates.update(f"{self.hint_text}you are not the author")

    def action_save_cloud(self) -> None:
        if (self.get_redact_or_visitor):
            self.editor.text = self.ta.text
            self.editor.save_document_cloud()
            self.updates.update(f"{self.hint_text}saved in cloud")
        else: self.updates.update(f"{self.hint_text}you are not the author")