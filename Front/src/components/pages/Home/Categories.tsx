import Link from 'next/link';
import { useRef } from 'react';
import { Container, ArrowButton } from '../../../styles/components/pages/Home/Categories';
import CategoryPlaceHolder from './placeholders/CategoryPlaceHolder';
import { useAxios } from '../../../hooks/useAxios';

interface ICategory {
  id: number;
  title: string;
  image_url: string;
}

export default function Categories(): JSX.Element {
  const { data } = useAxios<ICategory[]>('categories');
  const scrollRef = useRef<HTMLDivElement>(null);

  const scroll = (offset: number) => {
    if (scrollRef.current) {
      scrollRef.current.scrollBy({ left: offset, behavior: 'smooth' });
    }
  };

  if (!data) {
    return (
      <Container className="scroll-box">
        <div role="list" className="scroll-box__wrapper" ref={scrollRef}>
          <CategoryPlaceHolder repeatCount={13} />
        </div>
      </Container>
    );
  }

  return (
    <Container className="scroll-box">
      <ArrowButton direction="left" onClick={() => scroll(-200)}>&lt;</ArrowButton>
      <div role="list" className="scroll-box__wrapper" ref={scrollRef}>
        {data.map(category => (
          <main role="listitem" key={category.title}>
            <Link href={`categories/${category.title}`}>
              <span>{category.title}</span>
            </Link>
          </main>
        ))}
      </div>
      <ArrowButton direction="right" onClick={() => scroll(200)}>&gt;</ArrowButton>
    </Container>
  );
}
