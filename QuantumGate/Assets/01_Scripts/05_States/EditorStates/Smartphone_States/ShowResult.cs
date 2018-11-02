using UnityEngine;

/// <summary>
/// Etat pour montrer le résultat du circuit quantique jusqu'a la ligne _row.
/// </summary>
namespace Smartphone_States
{
    public class ShowResult : EditorState
    {
        /// <summary>
        /// Numero de la ligne
        /// </summary>
        private int _row;

        /// <summary>
        /// Etat precedent
        /// </summary>
        private EditorState _previousState;

        /// <summary>
        /// </summary>
        /// <param name="context">Le SandBoxManager qui est le contexte du jeu</param>
        /// <param name="row">Numero de la ligne où l'evaluation du circuit s'arrete</param>
        /// <param name="previousState">Etat precedent</param>
        public ShowResult(Editor context, int row, EditorState previousState) : base(context)
        {
            _row = row;
            _previousState = previousState;
        }

		public override void OnEnter()
		{

			string res, seq, realres;
			int taille, i;
			Debug.Log("ShowResult");
			//context.SetResultHeader("Result of row : " + _row);
			//context.SetResultText(context.currentCircuit.Evaluate(_row).ToString());

			res = context.currentCircuit.Evaluate(_row).ToString();
			//seq = context.currentCircuit.Evaluate(_row).Sequence();
			realres = context.BruteForce(res);


			context.SetResultText (realres);
			taille = res.Length;
			// A revoir si y a le temps
			if(taille > 50){
				context.SetresultPanelScale(new Vector3(1.0f, 1.0f, 1.0f)*1.1f);
				context.SetResultSize(25);
			}
			if(taille > 30 && taille <= 50){
				context.SetresultPanelScale(new Vector3(1.0f, 1.0f, 1.0f));
				context.SetResultSize(30);
			}
			if(taille <= 30){
				context.SetresultPanelScale(new Vector3(1.0f, 1.0f, 1.0f)*0.7f);//taille du nuage
				context.SetResultSize(50);// Modification du text de la boite des resultats
			}



			Debug.Log("taille ="+ taille);// message de test pour afficher la taille du résultat dans la console
			context.ShowResultPanel(true);

		}

        public override void OnExit() {
            context.gridBoard.DeselectRow(_row);
            context.ShowResultPanel(false);
        }

        public override void OnBackResultClick() { context.CurrentState = _previousState; }

        public override void OnBackButton() { OnBackResultClick(); }
    }
}
