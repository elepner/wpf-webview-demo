using System.Windows;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeWebView();
    }

    private async void InitializeWebView()
    {
        await MyWebView.EnsureCoreWebView2Async(null);
        string htmlContent = """"
            <html>

            <body>
              <button id="read-data">Read data</button>
              <div id="editor-container">
              </div>
            </body>
            <style>
              #editor-container {
                height: 375px;
                width: 100%;
                border: 1px solid cyan;
              }
            </style>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/quill/1.3.7/quill.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/quill/1.3.7/quill.min.js"></script>

            <!-- Theme included stylesheets -->
            <link href="https://cdnjs.cloudflare.com/ajax/libs/quill/1.3.7/quill.snow.css" rel="stylesheet">
            <link href="https://cdnjs.cloudflare.com/ajax/libs/quill/1.3.7/quill.bubble.css" rel="stylesheet">

            <script>
              console.log("Hello world");
              var quill = new Quill('#editor-container', {
                modules: {
                  toolbar: [
                    [{ header: [1, 2, false] }],
                    ['bold', 'italic', 'underline'],
                    ['image', 'code-block']
                  ]
                },
                placeholder: 'Compose an epic...',
                theme: 'snow'  // or 'bubble'
              });
              console.log(quill)
              document.getElementById('read-data').onclick = () => {
                document.body.append(getInputValue())
              }

              function getInputValue() {
                return quill.root.innerHTML;
              }
            </script>

            </html>
            """";
        
        MyWebView.CoreWebView2.NavigateToString(htmlContent);

        
    }

    private async void ReadInputButton_Click(object sender, RoutedEventArgs e)
    {
        string jsCode = "getInputValue();";
        string inputValue = await MyWebView.CoreWebView2.ExecuteScriptAsync(jsCode);
        MessageBox.Show("Input value: " + inputValue);
    }
}