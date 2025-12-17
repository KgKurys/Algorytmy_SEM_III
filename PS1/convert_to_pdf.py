import markdown
import os
from weasyprint import HTML, CSS

# Wczytaj plik markdown
with open('Sciaga_Matematyka_Dyskretna.md', 'r', encoding='utf-8') as f:
    md_content = f.read()

# Konwertuj markdown na HTML
html_content = markdown.markdown(
    md_content,
    extensions=[
        'extra',
        'codehilite',
        'tables',
        'fenced_code',
        'nl2br'
    ]
)

# Dodaj CSS dla lepszego wyglądu
css_style = """
<style>
    @page {
        size: A4;
        margin: 2cm;
    }
    body {
        font-family: 'Segoe UI', Arial, sans-serif;
        line-height: 1.6;
        color: #333;
        max-width: 100%;
    }
    h1 {
        color: #2c3e50;
        border-bottom: 3px solid #3498db;
        padding-bottom: 10px;
        page-break-before: always;
        margin-top: 30px;
    }
    h1:first-of-type {
        page-break-before: avoid;
    }
    h2 {
        color: #34495e;
        border-bottom: 2px solid #95a5a6;
        padding-bottom: 5px;
        margin-top: 25px;
    }
    h3 {
        color: #7f8c8d;
        margin-top: 20px;
    }
    code {
        background-color: #f4f4f4;
        padding: 2px 6px;
        border-radius: 3px;
        font-family: 'Consolas', 'Monaco', monospace;
        font-size: 0.9em;
    }
    pre {
        background-color: #f8f8f8;
        border: 1px solid #ddd;
        border-left: 3px solid #3498db;
        padding: 15px;
        overflow-x: auto;
        border-radius: 5px;
        page-break-inside: avoid;
    }
    pre code {
        background-color: transparent;
        padding: 0;
    }
    table {
        border-collapse: collapse;
        width: 100%;
        margin: 20px 0;
        page-break-inside: avoid;
    }
    th, td {
        border: 1px solid #ddd;
        padding: 12px;
        text-align: left;
    }
    th {
        background-color: #3498db;
        color: white;
        font-weight: bold;
    }
    tr:nth-child(even) {
        background-color: #f9f9f9;
    }
    blockquote {
        border-left: 4px solid #3498db;
        padding-left: 15px;
        margin-left: 0;
        color: #555;
        background-color: #f0f7fb;
        padding: 10px 15px;
    }
    ul, ol {
        margin-left: 20px;
    }
    li {
        margin: 5px 0;
    }
    hr {
        border: none;
        border-top: 2px solid #ecf0f1;
        margin: 30px 0;
    }
    .math {
        font-family: 'Cambria Math', 'Times New Roman', serif;
        font-size: 1.1em;
    }
    strong {
        color: #2c3e50;
    }
</style>
"""

# Pełny dokument HTML
full_html = f"""
<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <title>Ściąga - Matematyka Dyskretna</title>
    {css_style}
</head>
<body>
    {html_content}
</body>
</html>
"""

# Zapisz HTML (opcjonalnie)
with open('Sciaga_Matematyka_Dyskretna.html', 'w', encoding='utf-8') as f:
    f.write(full_html)

# Konwertuj do PDF
print("Konwersja do PDF...")
HTML(string=full_html).write_pdf('Sciaga_Matematyka_Dyskretna.pdf')

print("✓ PDF został utworzony: Sciaga_Matematyka_Dyskretna.pdf")
print("✓ HTML został utworzony: Sciaga_Matematyka_Dyskretna.html")
