namespace Common
{
    using UnityEngine;

    public static class EasingHandler
    {
        public static float Ease(float t, EasingType easing)
        {
            switch (easing)
            {
                case EasingType.InOutSine:
                    return InOutSine(t);
                case EasingType.InOutQuad:
                    return InOutQuad(t);
                case EasingType.InOutCubic:
                    return InOutCubic(t);
                case EasingType.InOutQuart:
                    return InOutQuart(t);
                case EasingType.InOutQuint:
                    return InOutQuint(t);
                case EasingType.InOutExpo:
                    return InOutExpo(t);
                case EasingType.InOutCirc:
                    return InOutCirc(t);
                case EasingType.InOutBack:
                    return InOutBack(t);
                case EasingType.InOutElastic:
                    return InOutElastic(t);
                default:
                    return t;
            }
        }

        private static float InOutSine(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }

        private static float InOutQuad(float t)
        {
            return t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }

        private static float InOutCubic(float t)
        {
            return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }

        private static float InOutQuart(float t)
        {
            return t < 0.5 ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
        }

        private static float InOutQuint(float t)
        {
            return t < 0.5 ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
        }

        private static float InOutExpo(float t)
        {
            return t == 0
              ? 0
              : t == 1
              ? 1
              : t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2
              : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
        }

        private static float InOutCirc(float t)
        {
            return t < 0.5
              ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
              : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
        }

        private static float InOutBack(float t)
        {
            const float n = 1.70158f * 1.525f;

            return t < 0.5
              ? (Mathf.Pow(2 * t, 2) * ((n + 1) * 2 * t - n)) / 2
              : (Mathf.Pow(2 * t - 2, 2) * ((n + 1) * (t * 2 - 2) + n) + 2) / 2;
        }

        private static float InOutElastic(float t)
        {
            const float n = (2 * Mathf.PI) / 4.5f;

            return t == 0
              ? 0
              : t == 1
              ? 1
              : t < 0.5
              ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * n)) / 2
              : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * n)) / 2 + 1;
        }
    }
}