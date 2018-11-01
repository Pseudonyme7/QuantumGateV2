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
            string res, seq;
            int taille;
            Debug.Log("ShowResult");
            //context.SetResultHeader("Result of row : " + _row);
            context.SetResultText(context.currentCircuit.Evaluate(_row).ToString());
            res = context.currentCircuit.Evaluate(_row).ToString();
            //seq = context.currentCircuit.Evaluate(_row).Sequence();
            /* if (seq[0]=0 && seq[1] = 0 && seq[2] = 0)
            GameObject ball1 = context.CreateBlackBall(268, 200, (float)0.3);
            GameObject ball2 = context.CreateBlackBall(300, 200, (float)0.3);
            GameObject ball3 = context.CreateBlackBall(332, 200, (float)0.3);

            GameObject ball4 = context.CreateBlackBall(268, 150, (float)0.3);
            GameObject ball5 = context.CreateBlackBall(300, 150, (float)0.3);
            GameObject ball6 = context.CreateBlackBall(332, 150, (float)0.3);

            GameObject ball7 = context.CreateBlackBall(268, 120, (float)0.3);
            GameObject ball8 = context.CreateBlackBall(300, 120, (float)0.3);
            GameObject ball9 = context.CreateBlackBall(332, 120, (float)0.3);

            GameObject ball10 = context.CreateBlackBall(268, 120, (float)0.3);
            GameObject ball11 = context.CreateBlackBall(300, 120, (float)0.3);
            GameObject ball12 = context.CreateBlackBall(332, 120, (float)0.3);

            GameObject ball13 = context.CreateBlackBall(268, 70, (float)0.3);
            GameObject ball14 = context.CreateBlackBall(300, 70, (float)0.3);
            GameObject ball15 = context.CreateBlackBall(332, 70, (float)0.3);
            */
            taille = res.Length;
            // A revoir si y a le temps
            // Modif du nuage
            context.SetresultPanelScale(new Vector3(1.0f, 1.0f, 1.0f));


            // message de test pour afficher la taille du résultat dans la console
            Debug.Log("taille =" + taille);
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
