using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace _5Laboras
{
    public partial class Form1 : System.Web.UI.Page
    {
        const string result = "Rezultatai.txt";

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Table1.Visible = false;
            Table2.Visible = false;
            Button2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            Table3.Visible = false;
            Table4.Visible = false;
            Label6.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;
            Button2.Visible = true;
            Label1.Visible = true;
            Table1.Visible = true;
            Table2.Visible = true;
            Table3.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            TextBox3.Visible = true;
            Label5.Visible = true;

            File.Delete(Server.MapPath(result));

            string error = "";

            List<Storage> conteiners = 
                InOut.ReadConteiners(Server.MapPath("Data"), ref error);

            List<Product> products = 
                InOut.ReadProducts(Server.MapPath("Leidiniai.csv"), ref error);

            if (error != "")
            {
                Label6.Visible = true;
                Label6.Text = error;
            }

            InOut.PrintConteiners(conteiners, 
                Server.MapPath(result), "Pradiniai duomenų failai:");
            InOut.PrintProducts(products, 
                Server.MapPath(result), "Pradinis leidinių failas:");

            StorageTables(conteiners);
            FillProductsTable(products);

            Session["pradCont"] = conteiners;
            Session["pradProd"] = products;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label1.Visible = true;
            Table1.Visible = true;
            Table2.Visible = true;
            Table3.Visible = true;
            Button2.Visible = false;
            Label2.Visible = true;
            Table4.Visible = true;

            List<Storage> conteiners = 
                (List<Storage>)Session["pradCont"];
            List<Product> products = 
                (List<Product>)Session["pradProd"];

            List<Prenumerator> allprenum = 
                TaskUtils.AllPrenum(conteiners);

            InOut.PrintOrderPrice(products, allprenum, 
                Server.MapPath(result), "Užsakymo kaina prenumeratoriui:");

            StorageTables(conteiners);
            FillProductsTable(products);
            FillTable2(products, allprenum);

            int month = int.Parse(TextBox1.Text);
            int startYear = int.Parse(TextBox2.Text);
            int endYear = int.Parse(TextBox3.Text);

            List<Prenumerator> selectedPrenumerators =
            TaskUtils.SelectedPrenumerators(conteiners,
            month, startYear, endYear);

            InOut.PrintSelected(selectedPrenumerators,
                Server.MapPath(result), $"Atrinkti prenumeratoriai nuo " +
                $"{startYear} iki {endYear} metų:");

            FillTable3(selectedPrenumerators);

            TaskUtils.ProductCount(selectedPrenumerators, products);

            InOut.PrintProducts(products,
            Server.MapPath(result), "Atrinktų leidinių kiekiai:");

            FillTable4(products);
        }

        /// <summary>
        /// Fill storage tables
        /// </summary>
        /// <param name="conteiner"></param>
        private void StorageTables(List<Storage> storages)
        {
            foreach (Storage storage in storages)
            {
                Table table = new Table();
                table.GridLines = GridLines.Horizontal;
                TableCell one = new TableCell();
                one.Text = storage.Year.ToString();
                TableRow rowone = new TableRow();
                rowone.Cells.Add(one);

                TableRow rowtwo = CaptionsConteinerTable();
                table.Rows.Add(rowone);
                table.Rows.Add(rowtwo);

                foreach (Prenumerator prenumerator
                    in storage.GetPrenumerators())
                {
                    TableRow rowthree = new TableRow();
                    TableCell two = new TableCell();
                    two.Text = prenumerator.Surname;
                    TableCell three = new TableCell();
                    three.Text = prenumerator.Address;
                    TableCell four = new TableCell();
                    four.Text = prenumerator.Start.ToString();
                    TableCell five = new TableCell();
                    five.Text = prenumerator.Duration.ToString();
                    TableCell six = new TableCell();
                    six.Text = prenumerator.Code;
                    TableCell seven = new TableCell();
                    seven.Text = prenumerator.Count.ToString();
                    rowthree.Cells.Add(two);
                    rowthree.Cells.Add(three);
                    rowthree.Cells.Add(four);
                    rowthree.Cells.Add(five);
                    rowthree.Cells.Add(six);
                    rowthree.Cells.Add(seven);

                    table.Rows.Add(rowthree);
                }

                tablePlaceholder1.Controls.Add(table);
            }
        }

        /// <summary>
        /// Returns the captions for the containers tables
        /// </summary>
        /// <returns></returns>
        private TableRow CaptionsConteinerTable()
        {
            TableCell one = new TableCell();
            one.Text = "Pavardė";
            TableCell two = new TableCell();
            two.Text = "Adresas";
            TableCell three = new TableCell();
            three.Text = "Pradžia";
            TableCell four = new TableCell();
            four.Text = "Trukmė";
            TableCell five = new TableCell();
            five.Text = "Kodas";
            TableCell six = new TableCell();
            six.Text = "Kiekis";
            TableRow row = new TableRow();
            row.Cells.Add(one);
            row.Cells.Add(two);
            row.Cells.Add(three);
            row.Cells.Add(four);
            row.Cells.Add(five);
            row.Cells.Add(six);

            return row;
        }

        /// <summary>
        /// Fills products table
        /// </summary>
        /// <param name="products"></param>
        private void FillProductsTable(List<Product> products)
        {
            Table1.Rows.Add(CaptionsProductTable());

            foreach (Product product in products)
            {
                TableRow rowthree = new TableRow();
                TableCell one = new TableCell();
                one.Text = product.Code;
                TableCell two = new TableCell();
                two.Text = product.Name;
                TableCell three = new TableCell();
                three.Text = product.Price.ToString();
                TableCell four = new TableCell();
                four.Text = product.Count.ToString();
                rowthree.Cells.Add(one);
                rowthree.Cells.Add(two);
                rowthree.Cells.Add(three);
                rowthree.Cells.Add(four);

                Table1.Rows.Add(rowthree);
            }
        }

        /// <summary>
        /// Returns the row with captions of the products table
        /// </summary>
        /// <returns></returns>
        private TableRow CaptionsProductTable()
        {
            TableCell one = new TableCell();
            one.Text = "Kodas";
            TableCell two = new TableCell();
            two.Text = "Pavadinimas";
            TableCell three = new TableCell();
            three.Text = "Kaina";
            TableCell four = new TableCell();
            four.Text = "Kiekis";
            TableRow row = new TableRow();
            row.Cells.Add(one);
            row.Cells.Add(two);
            row.Cells.Add(three);
            row.Cells.Add(four);

            return row;
        }

        /// <summary>
        /// Fills table2
        /// </summary>
        /// <param name="products"></param>
        /// <param name="prenumerators"></param>
        private void FillTable2(List<Product> products,
            List<Prenumerator> prenumerators)
        {
            TableCell caption = new TableCell();
            caption.Text = "Užsakymo kaina:";
            TableRow captionrow = new TableRow();
            captionrow.Cells.Add(caption);

            Table2.Rows.Add(captionrow);

            TableCell head = new TableCell();
            head.Text = "Pavardė";
            TableCell header = new TableCell();
            header.Text = "Kaina";
            TableRow headrow = new TableRow();
            headrow.Cells.Add(head);
            headrow.Cells.Add(header);

            Table2.Rows.Add(headrow);

            var updatedPrenum = prenumerators.Join(products,
                prenum => prenum.Code,
                prod => prod.Code,
                (prenum, prod) => new
                {
                    Subscriber = prenum.Surname,
                    Price = prod.Price * prenum.Count * prenum.Duration
                });

            foreach (var el in updatedPrenum)
            {
                TableCell one = new TableCell();
                one.Text = el.Subscriber;
                TableCell two = new TableCell();
                two.Text = el.Price.ToString();
                TableRow row = new TableRow();
                row.Cells.Add(one);
                row.Cells.Add(two);

                Table2.Rows.Add(row);
            }
        }

        /// <summary>
        /// Fills table5
        /// </summary>
        /// <param name="prenumerators"></param>
        private void FillTable3(List<Prenumerator> prenumerators)
        {
            TableCell header = new TableCell();
            header.Text = "Atrinkti prenumeratoriai pagal datą:";
            TableRow headrow = new TableRow();
            headrow.Cells.Add(header);

            Table3.Rows.Add(headrow);

            TableCell capone = new TableCell();
            capone.Text = "Pavardė";
            TableCell captwo = new TableCell();
            captwo.Text = "Adresas";
            TableCell capthree = new TableCell();
            capthree.Text = "Mėnesiai";
            TableRow caprow = new TableRow();
            caprow.Cells.Add(capone);
            caprow.Cells.Add(captwo);
            caprow.Cells.Add(capthree);

            Table3.Rows.Add(caprow);

            foreach (Prenumerator prenumerator in prenumerators)
            {
                TableCell one = new TableCell();
                one.Text = prenumerator.Surname;
                TableCell two = new TableCell();
                two.Text = prenumerator.Address;
                TableCell three = new TableCell();
                three.Text = FillMonths(prenumerator);
                TableRow row = new TableRow();
                row.Cells.Add(one);
                row.Cells.Add(two);
                row.Cells.Add(three);

                Table3.Rows.Add(row);
            }
        }

        /// <summary>
        /// Returns a line with ordered months
        /// </summary>
        /// <param name="prenumerator"></param>
        /// <returns></returns>
        private string FillMonths(Prenumerator prenumerator)
        {
            string line = "";

            for (int i = 1; i < 13; i++)
            {
                if (prenumerator.Start <= i
                    && prenumerator.Duration
                    + prenumerator.Start - 1 >= i)
                {
                    if (i == 12)
                    {
                        line += "*";
                    }
                    else
                    {
                        line += "* ";
                    }
                }
                else
                {
                    if (i == 12)
                    {
                        line += ".";
                    }
                    else
                    {
                        line += ". ";
                    }
                }
            }

            return line;
        }

        /// <summary>
        /// Fills table6
        /// </summary>
        /// <param name="products"></param>
        private void FillTable4(List<Product> products)
        {
            Table4.Rows.Add(CaptionsProductTable());

            foreach (Product product in products)
            {
                TableRow row = new TableRow();
                TableCell one = new TableCell();
                one.Text = product.Code;
                TableCell two = new TableCell();
                two.Text = product.Name;
                TableCell three = new TableCell();
                three.Text = product.Price.ToString();
                TableCell four = new TableCell();
                four.Text = product.Count.ToString();
                row.Cells.Add(one);
                row.Cells.Add(two);
                row.Cells.Add(three);
                row.Cells.Add(four);

                Table4.Rows.Add(row);
            }
        }
    }
}