import Link from 'next/link';
import Skeleton from 'react-loading-skeleton';
import { Container } from '../../../styles/components/pages/Home/Restaurant';

interface IRestaurantProps {
  restaurantData?: {
    id: number;
    title: string;
    image_url: string;
    category: string;
    distance: number;
    start_time: number;
    end_time: number;
    rating: number;
    price?: string;
  };
  isFamousContainer?: boolean;
  loading?: boolean;
}

export default function Restaurant({
  restaurantData,
  isFamousContainer,
  loading,
}: IRestaurantProps) {
  if (loading) {
    return (
      <Container isFamousContainer={isFamousContainer}>
        <div>
          <aside />
          <main>
            <h5>
              <Skeleton width={90} height={20} />
            </h5>
            <span>
              <Skeleton width={175} height={20} />
            </span>
            <span>
              <Skeleton width={70} height={20} />
            </span>
          </main>
        </div>
      </Container>
    );
  }

  if (!restaurantData) return null;

  const { image_url, title, category, distance, price } = restaurantData;

  const formattedDistance = distance.toFixed(2);
  // Exemplo simples: cada 1 km = 4 min
  const estimatedTime = Math.round(distance * 4);

  return (
    <Container isFamousContainer={isFamousContainer}>
      <Link href={`/restaurant/${title}`}>
        <div>
          <aside />
          <main>
            <h5>{title}</h5>
            <span>
              &nbsp;• {category} • {formattedDistance} km
            </span>
            <span>
              {estimatedTime} min • {price}
            </span>
          </main>
        </div>
      </Link>
    </Container>
  );
}
