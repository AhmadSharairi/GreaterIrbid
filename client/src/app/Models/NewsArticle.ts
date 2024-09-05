import { NewsImage } from "./NewsImage";

export interface NewsArticle
{
id: number;
title: string;
date: Date;
imageUrl: string;
description: string;
images: NewsImage[];
}
