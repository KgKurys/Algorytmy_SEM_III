import markdown
import os

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
        'nl2br',
        'toc'
    ]
)

# Dodaj CSS dla lepszego wyglądu i drukowania do PDF
css_style = """
<style>
    @media print {
        @page {
            size: A4;
            margin: 1.5cm;
        }
        h1 {
            page-break-before: always;
        }
        h1:first-of-type {
            page-break-before: avoid;
        }
        pre, table, blockquote {
            page-break-inside: avoid;
        }
    }
    
    body {
        font-family: 'Segoe UI', 'Calibri', Arial, sans-serif;
        line-height: 1.6;
        color: #2c3e50;
        max-width: 1000px;
        margin: 0 auto;
        padding: 20px;
        background-color: #ffffff;
    }
    
    h1 {
        color: #1a5490;
        border-bottom: 4px solid #3498db;
        padding-bottom: 12px;
        margin-top: 40px;
        margin-bottom: 20px;
        font-size: 2.2em;
        font-weight: 700;
    }
    
    h1:first-of-type {
        margin-top: 0;
        text-align: center;
        border-bottom: 5px double #3498db;
        padding-bottom: 20px;
    }
    
    h2 {
        color: #2c3e50;
        border-bottom: 2px solid #95a5a6;
        padding-bottom: 8px;
        margin-top: 30px;
        margin-bottom: 15px;
        font-size: 1.7em;
        font-weight: 600;
    }
    
    h3 {
        color: #34495e;
        margin-top: 20px;
        margin-bottom: 12px;
        font-size: 1.3em;
        font-weight: 600;
        border-left: 4px solid #3498db;
        padding-left: 12px;
    }
    
    h4 {
        color: #555;
        margin-top: 15px;
        font-size: 1.1em;
        font-weight: 600;
    }
    
    code {
        background-color: #f4f6f7;
        padding: 3px 7px;
        border-radius: 4px;
        font-family: 'Consolas', 'Monaco', 'Courier New', monospace;
        font-size: 0.9em;
        color: #c7254e;
        border: 1px solid #e1e4e8;
    }
    
    pre {
        background-color: #f8f9fa;
        border: 1px solid #d1d5da;
        border-left: 4px solid #3498db;
        padding: 16px;
        overflow-x: auto;
        border-radius: 6px;
        line-height: 1.45;
        margin: 15px 0;
    }
    
    pre code {
        background-color: transparent;
        padding: 0;
        border: none;
        color: #24292e;
        font-size: 0.85em;
    }
    
    table {
        border-collapse: collapse;
        width: 100%;
        margin: 20px 0;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        border-radius: 6px;
        overflow: hidden;
    }
    
    th {
        background: linear-gradient(135deg, #3498db 0%, #2980b9 100%);
        color: white;
        font-weight: 600;
        padding: 14px;
        text-align: left;
        font-size: 0.95em;
        text-transform: uppercase;
        letter-spacing: 0.5px;
    }
    
    td {
        border: 1px solid #e1e4e8;
        padding: 12px;
        text-align: left;
    }
    
    tr:nth-child(even) {
        background-color: #f8f9fa;
    }
    
    tr:hover {
        background-color: #e8f4f8;
    }
    
    blockquote {
        border-left: 5px solid #3498db;
        padding: 12px 20px;
        margin: 20px 0;
        background-color: #ecf6fc;
        border-radius: 4px;
        font-style: italic;
    }
    
    ul, ol {
        margin-left: 25px;
        margin-bottom: 15px;
    }
    
    li {
        margin: 8px 0;
        line-height: 1.6;
    }
    
    hr {
        border: none;
        border-top: 3px solid #ecf0f1;
        margin: 40px 0;
    }
    
    strong {
        color: #1a5490;
        font-weight: 600;
    }
    
    em {
        color: #555;
        font-style: italic;
    }
    
    a {
        color: #3498db;
        text-decoration: none;
        border-bottom: 1px dotted #3498db;
    }
    
    a:hover {
        color: #2980b9;
        border-bottom: 1px solid #2980b9;
    }
    
    /* Formatowanie dla wzorów matematycznych */
    .math {
        font-family: 'Cambria Math', 'Times New Roman', serif;
        font-size: 1.15em;
        color: #2c3e50;
    }
    
    /* Specjalne formatowanie dla przykładów */
    .example {
        background-color: #fff9e6;
        border-left: 4px solid #f39c12;
        padding: 15px;
        margin: 15px 0;
        border-radius: 4px;
    }
    
    /* Nagłówek dokumentu */
    .header {
        text-align: center;
        padding: 30px 0;
        border-bottom: 5px double #3498db;
        margin-bottom: 40px;
    }
    
    .header h1 {
        margin: 0;
        border: none;
        padding: 0;
    }
    
    .header p {
        color: #7f8c8d;
        font-size: 1.1em;
        margin: 10px 0 0 0;
    }
    
    /* Stopka */
    .footer {
        margin-top: 50px;
        padding-top: 20px;
        border-top: 2px solid #ecf0f1;
        text-align: center;
        color: #95a5a6;
        font-size: 0.9em;
    }
    
    /* Podświetlenie ważnych informacji */
    .important {
        background-color: #ffe6e6;
        border-left: 4px solid #e74c3c;
        padding: 12px 15px;
        margin: 15px 0;
        border-radius: 4px;
    }
    
    .success {
        background-color: #e8f8f5;
        border-left: 4px solid #27ae60;
        padding: 12px 15px;
        margin: 15px 0;
        border-radius: 4px;
    }
    
    /* Spis treści */
    #toc {
        background-color: #f8f9fa;
        border: 2px solid #3498db;
        padding: 20px 30px;
        margin: 30px 0;
        border-radius: 8px;
    }
    
    #toc ul {
        list-style-type: none;
        margin-left: 0;
    }
    
    #toc li {
        margin: 8px 0;
    }
    
    #toc a {
        font-weight: 500;
        border-bottom: none;
    }
</style>
"""

# Pełny dokument HTML
full_html = f"""
<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Ściąga - Matematyka Dyskretna - Algorytmy i Struktury Danych</title>
    {css_style}
    <script src="https://polyfill.io/v3/polyfill.min.js?features=es6"></script>
    <script id="MathJax-script" async src="https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js"></script>
    <script>
        window.MathJax = {{
            tex: {{
                inlineMath: [['$', '$']],
                displayMath: [['$$', '$$']],
                processEscapes: true
            }}
        }};
    </script>
</head>
<body>
    <div class="header">
        <h1>ŚCIĄGA - MATEMATYKA DYSKRETNA</h1>
        <p>Algorytmy i Struktury Danych - Pracownia Specjalistyczna</p>
    </div>
    
    {html_content}
    
    <div class="footer">
        <p><em>Ściąga opracowana na podstawie implementacji w C# oraz teorii matematyki dyskretnej</em></p>
        <p><strong>Algorytmy i Struktury Danych - Pracownia Specjalistyczna</strong></p>
        <p>© 2025 - Powodzenia na egzaminie!</p>
    </div>
    
    <script>
        // Dodaj przyciski do drukowania
        document.addEventListener('DOMContentLoaded', function() {{
            const printBtn = document.createElement('button');
            printBtn.textContent = '🖨️ Drukuj do PDF';
            printBtn.style.cssText = 'position: fixed; top: 20px; right: 20px; padding: 12px 24px; ' +
                'background: linear-gradient(135deg, #3498db 0%, #2980b9 100%); color: white; ' +
                'border: none; border-radius: 6px; cursor: pointer; font-weight: 600; ' +
                'box-shadow: 0 4px 6px rgba(0,0,0,0.1); z-index: 1000; font-size: 16px;';
            printBtn.onclick = function() {{ window.print(); }};
            document.body.appendChild(printBtn);
            
            // Ukryj przycisk podczas drukowania
            window.onbeforeprint = function() {{ printBtn.style.display = 'none'; }};
            window.onafterprint = function() {{ printBtn.style.display = 'block'; }};
        }});
    </script>
</body>
</html>
"""

# Zapisz HTML
output_file = 'Sciaga_Matematyka_Dyskretna.html'
with open(output_file, 'w', encoding='utf-8') as f:
    f.write(full_html)

print(f"✓ Plik HTML został utworzony: {output_file}")
print(f"✓ Otwórz plik w przeglądarce i użyj Ctrl+P aby zapisać jako PDF")
print(f"✓ W ustawieniach drukowania wybierz 'Zapisz jako PDF' jako drukarkę")
print(f"\nInstrukcja:")
print(f"1. Otwórz plik: {os.path.abspath(output_file)}")
print(f"2. Naciśnij Ctrl+P lub kliknij przycisk '🖨️ Drukuj do PDF' w prawym górnym rogu")
print(f"3. Wybierz 'Microsoft Print to PDF' lub 'Zapisz jako PDF'")
print(f"4. Gotowe!")
