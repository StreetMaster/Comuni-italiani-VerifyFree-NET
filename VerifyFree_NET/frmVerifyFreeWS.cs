using System;
using System.Windows.Forms;

namespace VerifyFree_NET
{

    /// <summary>
    /// Esempio di utilizzo del servizio WS VERIFY Free per la verifica e la normalizzazione degli indirizzi italiani 
    /// realizzato da StreetMaster Italia
    /// 
    /// L'end point del servizio è 
    ///     https://streetmaster.streetmaster.it/smws/verify_free?wsdl
    ///     
    /// Per l'utilizzo registrarsi sul sito http://streetmaster.it e richiedere la chiave per il servizio Verify Free solo localita' 
    /// 
    ///  2016 - Software by StreetMaster (c)
    /// </summary>
    public partial class frmVerifyFreeWS : Form
    {
        int currCand = 0;

        VerifyFreeWS.totOutFree totOutVerifyFreeWS;

        public frmVerifyFreeWS()
        {
            InitializeComponent();
        }

        private void btnCallVerifyFree_Click(object sender, EventArgs e)
        {

            if (txtKey.Text==String.Empty)
            {
                MessageBox.Show("E' necessario specificare una chiave valida per il servizio VERIFY Free");
                txtKey.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            // oggetto client per l'utilizzo del ws VERIFY Free
            var verifyFreeObj = new VerifyFreeWS.VerifyFreeClient();

            // classe di input
            var inVerifyFree = new VerifyFreeWS.inputVerifyFree
            {
                // valorizzazione input
                localita = txtInComune.Text,
                cap = txtInCap.Text,
                provincia = txtInProvincia.Text
            };

            // chiamata al servizio
            totOutVerifyFreeWS = verifyFreeObj.VerifyFree(inVerifyFree, txtKey.Text);

            // lettura campi generali del risultato
            txtOutEsito.Text = totOutVerifyFreeWS.norm.ToString();
            txtOutCodErr.Text = totOutVerifyFreeWS.codErr.ToString();
            txtOutNumCand.Text = totOutVerifyFreeWS.numCand.ToString();

            currCand = 0;

            // dettaglio del primo candidato se esiste
            // nella form di output e' riportato solo un sottoinsieme di tutti i valori restituiti
            if (totOutVerifyFreeWS.numCand > 0)
            {
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyFreeWS.outItem[currCand].scoreComune.ToString();
            }
            Cursor = Cursors.Default;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            // dettaglio del successivo candidato se esiste
            if (currCand > 0)
            {
                currCand-=1;
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyFreeWS.outItem[currCand].scoreComune.ToString();
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            // dettagli del precedente candidato se esiste
            if (currCand< totOutVerifyFreeWS.numCand-1)
            {
                currCand += 1;
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
                txtOutScoreComune.Text = totOutVerifyFreeWS.outItem[currCand].scoreComune.ToString();
            }
        }
    }
}
