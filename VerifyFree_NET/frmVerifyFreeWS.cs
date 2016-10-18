using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerifyFree_NET
{
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
                MessageBox.Show("E' necessario specificare una chiave valida per il servizio Verify Free");
                txtKey.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var verifyFreeObj = new VerifyFreeWS.VerifyFreeClient();

            var inVerifyFree = new VerifyFreeWS.inputVerifyFree();

            inVerifyFree.localita = txtInComune.Text;
            inVerifyFree.cap = txtInCap.Text;
            inVerifyFree.provincia = txtInProvincia.Text;


            totOutVerifyFreeWS = verifyFreeObj.VerifyFree(inVerifyFree, txtKey.Text);

            txtOutEsito.Text = totOutVerifyFreeWS.norm.ToString();
            txtOutCodErr.Text = totOutVerifyFreeWS.codErr.ToString();
            txtOutNumCand.Text = totOutVerifyFreeWS.numCand.ToString();

            currCand = 0;
            if (totOutVerifyFreeWS.numCand > 0)
            {
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
            }
            Cursor = Cursors.Default;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            if (currCand > 0)
            {
                currCand-=1;
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            if (currCand< totOutVerifyFreeWS.numCand-1)
            {
                currCand += 1;
                txtOutCap.Text = totOutVerifyFreeWS.outItem[currCand].cap;
                txtOutComune.Text = totOutVerifyFreeWS.outItem[currCand].comune;
                txtOutFrazione.Text = totOutVerifyFreeWS.outItem[currCand].frazione;
                txtOutProvincia.Text = totOutVerifyFreeWS.outItem[currCand].provincia;
            }
        }
    }
}
