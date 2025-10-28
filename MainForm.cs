// ============================================================
// Namn: Fredrik Beck-Norén
// E-post: fredrikbecknoren@gmail.com
// Kurs: L0002B – Inlämningsuppgift 3 (Windows Forms)
// Datum: 28/10-2025
//
// Huvudform med meny + registrering av personuppgifter.
// Tre textboxar: förnamn, efternamn, personnummer.
// En knapp: Kontrollera. Multi-line textbox visar resultat.
// Meny: Registrera person (fokuserar formuläret) och Avsluta.
// ============================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PersonApp
{
    public class MainForm : Form
    {
        private readonly MenuStrip menu = new();
        private readonly ToolStripMenuItem mArkiv = new("Meny");
        private readonly ToolStripMenuItem mRegistrera = new("Registrera person");
        private readonly ToolStripMenuItem mAvsluta = new("Avsluta");

        private readonly Label lblFör = new() { Text = "Förnamn:" };
        private readonly TextBox txtFör = new();

        private readonly Label lblEfter = new() { Text = "Efternamn:" };
        private readonly TextBox txtEfter = new();

        private readonly Label lblPnr = new() { Text = "Personnummer:" };
        private readonly TextBox txtPnr = new() { PlaceholderText = "YYMMDD-XXXX eller ÅÅÅÅMMDDXXXX" };

        private readonly Button btnKontroll = new() { Text = "Kontrollera" };

        private readonly Label lblResult = new() { Text = "Resultat:", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        private readonly TextBox txtResult = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };

        public MainForm()
        {
            Text = "Personregistrering (Uppgift 3)";
            ClientSize = new Size(520, 360);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10);

            // Meny
            menu.Items.Add(mArkiv);
            mArkiv.DropDownItems.Add(mRegistrera);
            mArkiv.DropDownItems.Add(new ToolStripSeparator());
            mArkiv.DropDownItems.Add(mAvsluta);
            MainMenuStrip = menu;

            // Layout
            Controls.Add(menu);

            int x1 = 24, x2 = 160, w = 300, h = 28, y = 50, gap = 36;
            lblFör.SetBounds(x1, y, 120, h);   txtFör.SetBounds(x2, y, w, h); y += gap;
            lblEfter.SetBounds(x1, y, 120, h); txtEfter.SetBounds(x2, y, w, h); y += gap;
            lblPnr.SetBounds(x1, y, 120, h);   txtPnr.SetBounds(x2, y, w, h); y += gap + 4;

            btnKontroll.SetBounds(x2, y, 120, 32);
            y += 44;

            lblResult.SetBounds(x1, y, 160, h);
            txtResult.SetBounds(x1, y + 28, 440, 150);

            Controls.AddRange(new Control[] {
                lblFör, txtFör, lblEfter, txtEfter, lblPnr, txtPnr, btnKontroll, lblResult, txtResult
            });

            // Händelser
            mRegistrera.Click += (_, __) => txtFör.Focus();
            mAvsluta.Click += (_, __) => Close();
            btnKontroll.Click += OnKontroll;
        }

        private void OnKontroll(object? sender, EventArgs e)
        {
            txtResult.Clear();

            var fn = txtFör.Text.Trim();
            var en = txtEfter.Text.Trim();
            var pn = txtPnr.Text.Trim();

            if (fn == "" || en == "" || pn == "")
            {
                MessageBox.Show("Fyll i förnamn, efternamn och personnummer.", "Saknas uppgifter",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Skapa min person och kör kontrollerna
            var p = new Person(fn, en, pn);

            bool giltigt = p.ÄrGiltigtPnr21(); // 21-algoritmen (mod10 med vikterna 2/1)
            string kön = p.Kön();

            txtResult.AppendText($"Namn: {p.Förnamn} {p.Efternamn}{Environment.NewLine}");
            txtResult.AppendText($"Personnummer: {p.Personnummer}{Environment.NewLine}");
            txtResult.AppendText($"Kön: {kön}{Environment.NewLine}");
            txtResult.AppendText(giltigt ? "Personnumret är GILTIGT (21-algoritmen)." 
                                         : "Personnumret är OGILTIGT (21-algoritmen).");
        }
    }
}
