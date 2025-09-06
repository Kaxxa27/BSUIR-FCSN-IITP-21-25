def generate_bg_grad_table_html(num_rows) -> None:
    """
        A function that generates an html document with a table
        where each row forms a gradient from black to white in its own style.
        :return: None
    """
    with open("gradient_table.html", "w") as html_file:
        html_file.write(
            """<!DOCTYPE html>
<html>
<head>
<style>
table {
  border-collapse: collapse;
  width: 100%;
}

td {
  border: 1px solid black;
  padding: 8px;
}

</style>
</head>
<body>
<table>
""")
        for i in range(num_rows):
            gradient_value = i * (255 / num_rows)
            style = f"background: rgb({gradient_value}, {gradient_value}, {gradient_value});"
            html_file.write(f'<tr style="{style}">')
            for j in range(1):
                html_file.write(f"<td>{style}</td>")
            html_file.write("</tr>\n")

        html_file.write("</table>\n")
        html_file.write("</body>\n")
        html_file.write("</html>")


if __name__ == "__main__":
    generate_bg_grad_table_html(0)


