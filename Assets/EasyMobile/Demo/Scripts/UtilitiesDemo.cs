/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyMobile;

namespace EasyMobile.Demo
{
    public class UtilitiesDemo : MonoBehaviour
    {
        public GameObject ignoreConstraints;
        public GameObject isDisabled;
        public GameObject annualRemainingRequests;
        public GameObject delayAfterInstallRemainingTime;
        public GameObject coolingOffRemainingTime;
        public DemoUtils demoUtils;

        void Update()
        {
            if (MobileNativeRatingRequest.IsDisplayConstraintIgnored())
                demoUtils.DisplayBool(ignoreConstraints, true, "Ignore Constraints: TRUE");
            else
                demoUtils.DisplayBool(ignoreConstraints, false, "Ignore Constraints: FALSE");

            if (!MobileNativeRatingRequest.IsRatingRequestDisabled())
                demoUtils.DisplayBool(isDisabled, true, "Popup Enabled");
            else
                demoUtils.DisplayBool(isDisabled, false, "Popup Disabled");

            int remainingRequests = MobileNativeRatingRequest.GetThisYearRemainingRequests();
            int remainingDelayAfterInstallation = MobileNativeRatingRequest.GetRemainingDelayAfterInstallation();
            int remainingCoolingOff = MobileNativeRatingRequest.GetRemainingCoolingOffDays();
    
            demoUtils.DisplayBool(annualRemainingRequests, remainingRequests > 0, "This Year Remaining Requests: " + remainingRequests);
            demoUtils.DisplayBool(delayAfterInstallRemainingTime, remainingDelayAfterInstallation <= 0, "Delay-After-Installation Remaining Days: " + remainingDelayAfterInstallation);
            demoUtils.DisplayBool(coolingOffRemainingTime, remainingCoolingOff <= 0, "Cooling-Off Remaining Days: " + remainingCoolingOff);
        }

        public void RequestRating()
        {
            if (MobileNativeRatingRequest.CanRequestRating())
                MobileNativeRatingRequest.RequestRating();
            else
                MobileNativeUI.Alert("Alert", "The rating popup could not be shown because it was disabled or the display constraints are not satisfied.");
        }

        public void RequestRatingLocalized()
        {
            if (!MobileNativeRatingRequest.CanRequestRating())
            {
                MobileNativeUI.Alert("Alert", "The rating popup could not be shown because it was disabled or the display constraints are not satisfied.");
                return;
            }

            // For demo purpose, we translated the default content into French using Google Translate!
            var localized = new RatingDialogContent(
                                "Évaluation " + RatingDialogContent.PRODUCT_NAME_PLACEHOLDER,
                                "Comment évalueriez-vous " + RatingDialogContent.PRODUCT_NAME_PLACEHOLDER + "?",
                                "C'est mauvais. Souhaitez-vous nous donner quelques commentaires à la place?",
                                "Impressionnant! Faisons le!",
                                "Pas maintenant",
                                "Non",
                                "Évaluez maintenant!",
                                "Annuler",
                                "Réaction"
                            );

        
            MobileNativeRatingRequest.RequestRating(localized);
        }
    }
}
